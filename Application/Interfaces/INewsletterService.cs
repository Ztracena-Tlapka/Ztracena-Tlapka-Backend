using Ztracena_Tlapka_Backend.Application.DTOs;

namespace Ztracena_Tlapka_Backend.Application.Interfaces;

public interface INewsletterService
{
    Task<NewsletterResponse?> AddSubscriberAsync(string email);
    Task<IEnumerable<NewsletterResponse>> GetAllAsync();
    Task<NewsletterResponse?> GetByEmailAsync(string email);
    Task<NewsletterResponse?> GetByIdAsync(Guid id);
    Task<bool> UnsubscribeAsync(string email);
    Task<string?> GenerateConfirmationTokenAsync(string email);
    Task<bool> ConfirmAsync(string token);
    Task<bool> EmailExistsAsync(string email);
}
