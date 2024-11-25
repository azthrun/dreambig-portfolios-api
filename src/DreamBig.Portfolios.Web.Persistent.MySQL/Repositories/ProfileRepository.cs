using Dapper;
using DreamBig.Portfolios.Web.Application.Abstractions;
using DreamBig.Portfolios.Web.Domain.Entities;
using DreamBig.Portfolios.Web.Persistent.MySQL.Contexts;
using Microsoft.Extensions.Logging;

namespace DreamBig.Portfolios.Web.Persistent.MySQL.Repositories;

internal sealed class ProfileRepository(PortfoliosWebContext dbContext, ILogger<ProfileRepository>? logger) : IProfileRepository
{
    private readonly PortfoliosWebContext _dbContext = dbContext;
    private readonly ILogger<ProfileRepository>? _logger = logger;

    [DapperAot]
    public async Task<Profile?> GetProfileAsync()
    {
        try
        {
            _logger?.LogDebug("Getting profile from database");
            using var connection = _dbContext.GetConnection();
            var query = "SELECT * FROM Profiles LIMIT 1";
            var results = await connection.QueryFirstOrDefaultAsync<Profile>(query);
            return results;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error getting profile from database");
            throw;
        }
    }
}
