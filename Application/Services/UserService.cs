using Ztracena_Tlapka_Backend.Application.DTOs;
using Ztracena_Tlapka_Backend.Application.Interfaces;
using Ztracena_Tlapka_Backend.Domain.Entities;

namespace Ztracena_Tlapka_Backend.Application.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<IEnumerable<UserResponse>> GetAllAsync() =>
        (await repository.GetAllAsync()).Select(ToResponse);

    public async Task<UserResponse?> GetByIdAsync(Guid id)
    {
        var user = await repository.GetByIdAsync(id);
        
        return user is null ? null : ToResponse(user);
    }

    public async Task<bool> EmailExistsAsync(string email) =>
        await repository.GetByEmailAsync(email) is not null;

    public async Task<UserResponse> CreateAsync(CreateUserRequest request)
    {
        var user = new User
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

        return ToResponse(await repository.CreateAsync(user));
    }

    public async Task<UserResponse?> UpdateAsync(Guid id, UpdateUserRequest request)
    {
        var user = await repository.GetByIdAsync(id);
        if (user is null) return null;

        if (request.FirstName is not null) user.FirstName = request.FirstName;
        if (request.LastName is not null) user.LastName = request.LastName;
        if (request.Phone is not null) user.Phone = request.Phone;
        if (request.Region is not null) user.Region = request.Region;
        if (request.District is not null) user.District = request.District;
        if (request.City is not null) user.City = request.City;
        if (request.Street is not null) user.Street = request.Street;
        if (request.PostalCode is not null) user.PostalCode = request.PostalCode;
        if (request.NewsletterSubscribed is not null) user.NewsletterSubscribed = request.NewsletterSubscribed.Value;
        user.UpdatedAt = DateTime.UtcNow;

        return ToResponse(await repository.UpdateAsync(user));
    }

    public Task<bool> DeleteAsync(Guid id) => repository.DeleteAsync(id);

    private static UserResponse ToResponse(User u) => new(
        u.Id, u.FirstName, u.LastName, u.Email,
        u.Phone, u.Region, u.District, u.City, u.Street, u.PostalCode,
        u.NewsletterSubscribed, u.CreatedAt, u.UpdatedAt
    );
}
