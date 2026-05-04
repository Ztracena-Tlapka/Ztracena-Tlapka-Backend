namespace Ztracena_Tlapka_Backend.Application.Interfaces;

public interface INewsletterTokenService
{
    Task<string> GenerateTokenAsync(string email);
    Task<string?> ValidateAndConsumeTokenAsync(string token);
}
