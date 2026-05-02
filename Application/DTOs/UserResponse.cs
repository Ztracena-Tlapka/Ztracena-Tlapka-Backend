namespace Ztracena_Tlapka_Backend.Application.DTOs;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Region,
    string District,
    string City,
    string? Street,
    string? PostalCode,
    bool NewsletterSubscribed,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
