using Ztracena_Tlapka_Backend.Application.DTOs;
using Ztracena_Tlapka_Backend.Application.Interfaces;
using Ztracena_Tlapka_Backend.Domain.Entities;

namespace Ztracena_Tlapka_Backend.Application.Services;

public class AuthService(IUserRepository repository) : IAuthService
{
    public async Task<UserSessionData?> ValidateCredentialsAsync(string email, string password)
    {
        var user = await repository.GetByEmailAsync(email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return new UserSessionData(user.Id, user.FirstName, user.LastName, user.Email);
    }

    public async Task<UserResponse> RegisterAsync(RegisterUserRequest request)
    {
        var userData = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Phone = request.Phone,
            Region = request.Region,
            District = request.District,
            City = request.City,
            Street = request.Street,
            PostalCode = request.PostalCode,
            NewsletterSubscribed = request.NewsletterSubscribed,
            UpdatedAt = DateTime.UtcNow,
        };

        var user = await repository.CreateAsync(userData);
        
        return new (
            user.Id, user.FirstName, user.LastName, user.Email,
            user.Phone, user.Region, user.District, user.City, user.Street, user.PostalCode,
            user.NewsletterSubscribed, user.CreatedAt, user.UpdatedAt
        );
    }
}
