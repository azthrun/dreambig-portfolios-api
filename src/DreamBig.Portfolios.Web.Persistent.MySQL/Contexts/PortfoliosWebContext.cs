using DreamBig.Portfolios.Web.Domain.Models;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace DreamBig.Portfolios.Web.Persistent.MySQL.Contexts;

public sealed class PortfoliosWebContext(Secrets secrets, ILogger<PortfoliosWebContext>? logger)
{
    private readonly Secrets _secrets = secrets;
    private readonly ILogger<PortfoliosWebContext>? _logger = logger;

    public async Task<MySqlConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        _logger?.LogDebug("Opening MySQL connection");
        MySqlConnection connection = new(_secrets.MySqlConnectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}