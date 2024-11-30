using Dapper;
using DreamBig.Portfolios.Web.Application.Abstractions;
using DreamBig.Portfolios.Web.Domain.Entities;
using DreamBig.Portfolios.Web.Persistent.MySQL.Contexts;
using Microsoft.Extensions.Logging;

namespace DreamBig.Portfolios.Web.Persistent.MySQL.Repositories;

internal sealed class ExperienceRepository(PortfoliosWebContext dbContext, ILogger<ExperienceRepository>? logger) : IExperienceRepository
{
    private readonly PortfoliosWebContext _dbContext = dbContext;
    private readonly ILogger<ExperienceRepository>? _logger = logger;

    [DapperAot]
    public async Task<IEnumerable<Experience>> GetExperiencesAsync(string profileId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger?.LogDebug("Getting experiences from database for profile {ProfileId}", profileId);
            using var connection = await _dbContext.GetConnectionAsync(cancellationToken);
            var query = "SELECT * FROM Experiences WHERE profile_id = @profileId";
            var results = await connection.QueryAsync<Experience>(query, new { profileId });
            return results;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error getting experiences from database for profile {ProfileId}", profileId);
            throw;
        }
    }
}
