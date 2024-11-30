using Dapper;
using DreamBig.Portfolios.Web.Application.Abstractions;
using DreamBig.Portfolios.Web.Domain.Entities;
using DreamBig.Portfolios.Web.Persistent.MySQL.Contexts;
using Microsoft.Extensions.Logging;

namespace DreamBig.Portfolios.Web.Persistent.MySQL.Repositories;

internal sealed class PostRepository(PortfoliosWebContext dbContext, ILogger<PostRepository>? logger) : IPostRepository
{
    private readonly PortfoliosWebContext _dbContext = dbContext;
    private readonly ILogger<PostRepository>? _logger = logger;

    [DapperAot]
    public async Task<IEnumerable<Post>> GetPostsAsync(string profileId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger?.LogDebug("Getting posts from database for profile {ProfileId}", profileId);
            using var connection = await _dbContext.GetConnectionAsync(cancellationToken);
            var query = "SELECT * FROM Posts WHERE profile_id = @profileId";
            var results = await connection.QueryAsync<Post>(query, new { profileId });
            return results;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error getting posts from database for profile {ProfileId}", profileId);
            throw;
        }
    }
}
