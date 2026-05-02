using Microsoft.AspNetCore.Mvc;
using Ztracena_Tlapka_Backend.Application.Common;
using Ztracena_Tlapka_Backend.Application.DTOs;
using Ztracena_Tlapka_Backend.Application.Interfaces;

namespace Ztracena_Tlapka_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, ISessionService sessionService) : ControllerBase
{
    private const string CookieName = "session";

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var sessionData = await authService.ValidateCredentialsAsync(request.Email, request.Password)
            ?? throw new AppException<object>(401, new { message = "Invalid credentials" }, ResCodes.Auth.InvalidCredentials);

        var sessionId = await sessionService.CreateSessionAsync(sessionData, request.StayLoggedIn);

        Response.Cookies.Append(CookieName, sessionId, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            MaxAge = request.StayLoggedIn ? TimeSpan.FromDays(30) : TimeSpan.FromDays(1)
        });

        return Ok(ApiResponse.Success(new { message = "Logged in successfully", user = sessionData }, ResCodes.Auth.LoggedIn));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var sessionId = Request.Cookies[CookieName];

        if (sessionId is not null)
            await sessionService.RemoveSessionAsync(sessionId);

        Response.Cookies.Delete(CookieName);

        return Ok(ApiResponse.Success(new { message = "Logged out successfully" }, ResCodes.Auth.LoggedOut));
    }

    [HttpGet("verify")]
    public async Task<IActionResult> Verify()
    {
        var sessionId = Request.Cookies[CookieName];

        if (sessionId is null)
            throw new AppException<object>(401, new { message = "Not authenticated" }, ResCodes.Auth.NotAuthenticated);

        var session = await sessionService.GetSessionAsync(sessionId);

        if (session is null)
            throw new AppException<object>(401, new { message = "Not authenticated" }, ResCodes.Auth.NotAuthenticated);

        return Ok(ApiResponse.Success(new { message = "Authenticated", user = session }, ResCodes.Auth.Verified));
    }
}
