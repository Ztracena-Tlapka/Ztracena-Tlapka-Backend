namespace Ztracena_Tlapka_Backend.Application.DTOs;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    string Region,
    string District,
    string City,
    string? Street,
    string? PostalCode,
    bool NewsletterSubscribed
);
