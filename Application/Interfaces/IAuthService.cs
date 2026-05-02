using Ztracena_Tlapka_Backend.Application.DTOs;
using Ztracena_Tlapka_Backend.Domain.Entities;

namespace Ztracena_Tlapka_Backend.Application.Interfaces;

public interface IAuthService
{
    Task<UserSessionData?> ValidateCredentialsAsync(string email, string password);
    
    Task<UserResponse> RegisterAsync(RegisterUserRequest request);
    
}
