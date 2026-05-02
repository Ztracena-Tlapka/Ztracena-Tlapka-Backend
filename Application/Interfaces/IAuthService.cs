using Ztracena_Tlapka_Backend.Application.DTOs;

namespace Ztracena_Tlapka_Backend.Application.Interfaces;

public interface IAuthService
{
    Task<UserSessionData?> ValidateCredentialsAsync(string email, string password);
}
