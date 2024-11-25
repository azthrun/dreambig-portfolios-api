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
    public async Task<IEnumerable<Contact>> GetContactsAsync(string profileId)
    {
        try
        {
            _logger?.LogDebug("Getting contacts from database for profile {profileId}", profileId);
            using var connection = _dbContext.GetConnection();
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
    public async Task<ContactType?> GetContactTypeAsync(string contactTypeId)
    {
        try
        {
            _logger?.LogDebug("Getting contact type from database for contact type {contactTypeId}", contactTypeId);
            using var connection = _dbContext.GetConnection();
            var query = "SELECT * FROM ContactTypes WHERE contact_type_id = @contactTypeId";
            var results = await connection.QueryFirstOrDefaultAsync<ContactType>(query, new { contactTypeId });
            return results;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error getting contact type from database for contact type {contactTypeId}", contactTypeId);
            throw;
        }
    }
}
