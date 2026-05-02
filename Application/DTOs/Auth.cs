namespace Ztracena_Tlapka_Backend.Application.DTOs;

public record LoginRequest(string Email, string Password, bool StayLoggedIn);

public record UserSessionData(Guid Id, string FirstName, string LastName, string Email);
