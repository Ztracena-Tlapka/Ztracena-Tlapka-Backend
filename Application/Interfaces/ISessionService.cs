using Ztracena_Tlapka_Backend.Application.DTOs;

namespace Ztracena_Tlapka_Backend.Application.Interfaces;

public interface ISessionService
{
    Task<string> CreateSessionAsync(UserSessionData data, bool stayLoggedIn);
    Task<UserSessionData?> GetSessionAsync(string sessionId);
    Task RemoveSessionAsync(string sessionId);
}
