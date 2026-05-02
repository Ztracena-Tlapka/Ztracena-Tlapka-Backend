using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ztracena_Tlapka_Backend.Application.Common;
using Ztracena_Tlapka_Backend.Application.Interfaces;

namespace Ztracena_Tlapka_Backend.Api.Filters;

public class RequireAuthAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sessionId = context.HttpContext.Request.Cookies["session"];

        if (sessionId is not null)
        {
            var sessionService = context.HttpContext.RequestServices.GetRequiredService<ISessionService>();
            var session = await sessionService.GetSessionAsync(sessionId);

            if (session is not null)
            {
                context.HttpContext.Items["Session"] = session;
                await next();
                return;
            }
        }

        context.Result = new ObjectResult(
            ApiResponse.Error(new { message = "Not authenticated" }, ResCodes.Auth.NotAuthenticated))
        {
            StatusCode = 401
        };
    }
}
