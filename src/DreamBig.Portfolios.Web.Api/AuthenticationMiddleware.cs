
using DreamBig.Portfolios.Web.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace DreamBig.Portfolios.Web.Api;

internal sealed class AuthenticationMiddleware(ISessionRepository sessionRepository) : IMiddleware
{
    private readonly ISessionRepository _sessionRepository = sessionRepository;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() is not null)
        {
            await next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Authorization header is missing");
            return;
        }

        var token = authorizationHeader.ToString().Replace("Basic ", "");
        var isValid = await _sessionRepository.IsSessionIdValidAsync(token);
        if (!isValid)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid session");
            return;
        }

        await next(context);
    }
}