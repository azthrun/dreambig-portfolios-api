using Dapper;
using DreamBig.Portfolios.Web.Application.Abstractions;
using DreamBig.Portfolios.Web.Persistent.MySQL.Contexts;
using Microsoft.Extensions.Logging;

namespace DreamBig.Portfolios.Web.Persistent.MySQL.Repositories;

internal sealed class SessionRepository(PortfoliosWebContext dbContext, ILogger<SessionRepository>? logger) : ISessionRepository
{
    private readonly PortfoliosWebContext _dbContext = dbContext;
    private readonly ILogger<SessionRepository>? _logger = logger;

    [DapperAot]
    public async Task<string?> CreateSessionAsync(string? userAgent, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger?.LogDebug("Creating session record in database for User Agent string: {userAgent}", userAgent);
            using var connection = await _dbContext.GetConnectionAsync(cancellationToken);
            var id = Guid.NewGuid();
            var query = "INSERT INTO Sessions (id, user_agent, create_time, update_time) VALUES (@id, @userAgent, @createTime, @updateTime)";
            await connection.ExecuteAsync(query, new { id, userAgent, createTime = DateTime.UtcNow, updateTime = DateTime.UtcNow });
            return id.ToString();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error creating session record in database for User Agent string: {userAgent}", userAgent);
            throw;
        }
    }

    [DapperAot]
    public async Task<bool> IsSessionIdValidAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger?.LogDebug("Validating session in database for session Id: {sessionId}", sessionId);
            using var connection = await _dbContext.GetConnectionAsync(cancellationToken);
            var query = "SELECT COUNT(1) FROM Sessions WHERE id = @sessionId";
            var results = await connection.QueryFirstAsync<int>(query, new { sessionId });
            return results != 0;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error validating session in database for session Id: {sessionId}", sessionId);
            throw;
        }
    }
}
