using Ztracena_Tlapka_Backend.Domain.Entities;

namespace Ztracena_Tlapka_Backend.Application.Interfaces;

public interface INewsletterRepository
{
    Task<NewsletterSubscribers> CreateAsync(NewsletterSubscribers subscriber);
    Task<IEnumerable<NewsletterSubscribers>> GetAllAsync();
    Task<NewsletterSubscribers?> GetByIdAsync(Guid id);
    Task<NewsletterSubscribers?> GetByEmailAsync(string email);
    Task<bool> DeleteAsync(string email);
    Task<bool> DeleteAsync(Guid id);
    Task<NewsletterSubscribers> UpdateAsync(NewsletterSubscribers subscriber);
}
