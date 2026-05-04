namespace Ztracena_Tlapka_Backend.Domain.Entities;

public class NewsletterSubscribers
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; }
    public DateTime SubscribedAt { get; set; }
}
