using Microsoft.Extensions.Caching.Distributed;
using Ztracena_Tlapka_Backend.Application.Interfaces;

namespace Ztracena_Tlapka_Backend.Application.Services;

public class NewsletterTokenService(IDistributedCache cache) : INewsletterTokenService
{
    private static string Key(string token) => $"newsletter:confirm:{token}";

    public async Task<string> GenerateTokenAsync(string email)
    {
        var token = Guid.NewGuid().ToString("N");

        await cache.SetStringAsync(Key(token), email, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
        });

        return token;
    }

    public async Task<string?> ValidateAndConsumeTokenAsync(string token)
    {
        var key = Key(token);
        var email = await cache.GetStringAsync(key);

        if (email is null) return null;

        await cache.RemoveAsync(key);
        return email;
    }
}
