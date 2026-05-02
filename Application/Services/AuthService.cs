using Ztracena_Tlapka_Backend.Application.DTOs;
using Ztracena_Tlapka_Backend.Application.Interfaces;

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
}
