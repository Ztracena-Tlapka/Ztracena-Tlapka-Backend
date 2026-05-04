namespace Ztracena_Tlapka_Backend.Application.DTOs;

public record NewsletterResponse(
    Guid Id,
    string Email,
    bool IsSubscribed,
    DateTime SubscribedAt
);
