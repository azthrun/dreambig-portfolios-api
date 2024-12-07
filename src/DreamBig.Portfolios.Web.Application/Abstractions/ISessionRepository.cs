namespace DreamBig.Portfolios.Web.Application.Abstractions;

public interface ISessionRepository
{
    Task<string?> CreateSessionAsync(string? userAgent, CancellationToken cancellationToken = default);
    Task<bool> IsSessionIdValidAsync(string sessionId, CancellationToken cancellationToken = default);
}