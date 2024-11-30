using Dapper;
using DreamBig.Portfolios.Web.Application.Abstractions;
using DreamBig.Portfolios.Web.Domain.Entities;
using DreamBig.Portfolios.Web.Persistent.MySQL.Contexts;
using Microsoft.Extensions.Logging;

namespace DreamBig.Portfolios.Web.Persistent.MySQL.Repositories;

public sealed class ContactRepository(PortfoliosWebContext dbContext, ILogger<ContactRepository>? logger) : IContactRepository
{
    private readonly PortfoliosWebContext _dbContext = dbContext;
    private readonly ILogger<ContactRepository>? _logger = logger;

    [DapperAot]
    public async Task<IEnumerable<Contact>> GetContactsAsync(string profileId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger?.LogDebug("Getting contacts from database for profile {profileId}", profileId);
            using var connection = await _dbContext.GetConnectionAsync(cancellationToken);
            var query = "SELECT * FROM Contacts WHERE profile_id = @profileId";
            var results = await connection.QueryAsync<Contact>(query, new { profileId });
            return results;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error getting contacts from database for profile {profileId}", profileId);
            throw;
        }
    }

    [DapperAot]
    public async Task<IEnumerable<ContactType>> GetContactTypesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger?.LogDebug("Getting contact types from database");
            using var connection = await _dbContext.GetConnectionAsync(cancellationToken);
            var query = "SELECT * FROM ContactTypes";
            var results = await connection.QueryAsync<ContactType>(query);
            return results;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error getting contact type from database");
            throw;
        }
    }
}
