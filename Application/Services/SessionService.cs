using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Ztracena_Tlapka_Backend.Application.DTOs;
using Ztracena_Tlapka_Backend.Application.Interfaces;

namespace Ztracena_Tlapka_Backend.Application.Services;

public class SessionService(IDistributedCache cache) : ISessionService
{
    public async Task<string> CreateSessionAsync(UserSessionData data, bool stayLoggedIn)
    {
        var sessionId = Guid.NewGuid().ToString();

        await cache.SetStringAsync(sessionId, JsonSerializer.Serialize(data), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = stayLoggedIn ? TimeSpan.FromDays(30) : TimeSpan.FromDays(1)
        });

        return sessionId;
    }

    public async Task<UserSessionData?> GetSessionAsync(string sessionId)
    {
        var json = await cache.GetStringAsync(sessionId);
        
        return json is null ? null : JsonSerializer.Deserialize<UserSessionData>(json);
    }

    public Task RemoveSessionAsync(string sessionId) =>
        cache.RemoveAsync(sessionId);
}
