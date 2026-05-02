namespace Ztracena_Tlapka_Backend.Application.DTOs;

public record RegisterUserRequest(
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

public record LoginRequest(string Email, string Password, bool StayLoggedIn);

public record UserSessionData(Guid Id, string FirstName, string LastName, string Email);
