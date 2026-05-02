using Ztracena_Tlapka_Backend.Application.Common;

namespace Ztracena_Tlapka_Backend.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppException<object> exception)
        {
            context.Response.StatusCode = exception.StatusCode;
            context.Response.ContentType = "application/json";
            
            await context.Response.WriteAsJsonAsync(ApiResponse.Error(exception.Data, exception.ResCode));
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            
            await context.Response.WriteAsJsonAsync(ApiResponse.Error(new { message = exception.Message }));
        }
    }
}
