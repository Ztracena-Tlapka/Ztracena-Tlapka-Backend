using Microsoft.EntityFrameworkCore;
using Ztracena_Tlapka_Backend.Application.Interfaces;
using Ztracena_Tlapka_Backend.Domain.Entities;
using Ztracena_Tlapka_Backend.Infrastructure.Persistence;

namespace Ztracena_Tlapka_Backend.Infrastructure.Repositories;

public class NewsletterRepository(AppDbContext db) : INewsletterRepository
{
    public async Task<IEnumerable<NewsletterSubscribers>> GetAllAsync() =>
        await db.NewsletterSubscribers.ToListAsync();

    public async Task<NewsletterSubscribers?> GetByEmailAsync(string email) =>
        await db.NewsletterSubscribers.FirstOrDefaultAsync(s => s.Email == email);

    public async Task<NewsletterSubscribers?> GetByIdAsync(Guid id) =>
        await db.NewsletterSubscribers.FindAsync(id);

    public async Task<NewsletterSubscribers> CreateAsync(NewsletterSubscribers subscriber)
    {
        db.NewsletterSubscribers.Add(subscriber);
        await db.SaveChangesAsync();

        return subscriber;
    }

    public async Task<bool> DeleteAsync(string email)
    {
        var subscriber = await db.NewsletterSubscribers.FirstOrDefaultAsync(s => s.Email == email);

        if (subscriber is null) return false;

        db.NewsletterSubscribers.Remove(subscriber);
        await db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var subscriber = await db.NewsletterSubscribers.FindAsync(id);

        if (subscriber is null) return false;

        db.NewsletterSubscribers.Remove(subscriber);
        await db.SaveChangesAsync();

        return true;
    }

    public async Task<NewsletterSubscribers> UpdateAsync(NewsletterSubscribers subscriber)
    {
        db.NewsletterSubscribers.Update(subscriber);
        await db.SaveChangesAsync();

        return subscriber;
    }
}
