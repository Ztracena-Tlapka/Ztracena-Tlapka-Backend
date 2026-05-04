using Ztracena_Tlapka_Backend.Application.DTOs;
using Ztracena_Tlapka_Backend.Application.Interfaces;
using Ztracena_Tlapka_Backend.Domain.Entities;

namespace Ztracena_Tlapka_Backend.Application.Services;

public class NewsletterService(INewsletterRepository repository, INewsletterTokenService tokenService) : INewsletterService
{
    public async Task<NewsletterResponse?> AddSubscriberAsync(string email)
    {
        if (await EmailExistsAsync(email)) return null;

        var subscriber = new NewsletterSubscribers
        {
            Id = Guid.NewGuid(),
            Email = email,
            IsSubscribed = false,
            SubscribedAt = DateTime.UtcNow
        };

        return ToResponse(await repository.CreateAsync(subscriber));
    }

    public async Task<IEnumerable<NewsletterResponse>> GetAllAsync() =>
        (await repository.GetAllAsync()).Select(ToResponse);

    public async Task<NewsletterResponse?> GetByEmailAsync(string email)
    {
        var subscriber = await repository.GetByEmailAsync(email);

        return subscriber is null ? null : ToResponse(subscriber);
    }

    public async Task<NewsletterResponse?> GetByIdAsync(Guid id)
    {
        var subscriber = await repository.GetByIdAsync(id);

        return subscriber is null ? null : ToResponse(subscriber);
    }

    public async Task<bool> UnsubscribeAsync(string email) =>
        await repository.DeleteAsync(email);

    public async Task<string?> GenerateConfirmationTokenAsync(string email)
    {
        var subscriber = await repository.GetByEmailAsync(email);
        if (subscriber is null || subscriber.IsSubscribed) return null;

        return await tokenService.GenerateTokenAsync(email);
    }

    public async Task<bool> ConfirmAsync(string token)
    {
        var email = await tokenService.ValidateAndConsumeTokenAsync(token);
        if (email is null) return false;

        var subscriber = await repository.GetByEmailAsync(email);
        if (subscriber is null) return false;

        subscriber.IsSubscribed = true;
        await repository.UpdateAsync(subscriber);

        return true;
    }

    public async Task<bool> EmailExistsAsync(string email) =>
        await repository.GetByEmailAsync(email) is not null;

    private static NewsletterResponse ToResponse(NewsletterSubscribers s) =>
        new(s.Id, s.Email, s.IsSubscribed, s.SubscribedAt);
}
