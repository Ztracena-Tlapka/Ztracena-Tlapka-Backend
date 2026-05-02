namespace Ztracena_Tlapka_Backend.Application.DTOs;

public record UpdateUserRequest(
    string? FirstName,
    string? LastName,
    string? Phone,
    string? Region,
    string? District,
    string? City,
    string? Street,
    string? PostalCode,
    bool? NewsletterSubscribed
);
