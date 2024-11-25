using System.Data;
using DreamBig.Portfolios.Web.Domain.Models;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace DreamBig.Portfolios.Web.Persistent.MySQL.Contexts;

public sealed class PortfoliosWebContext(Secrets secrets, ILogger<PortfoliosWebContext>? logger)
{
    private readonly MySqlConnection _connection = new(secrets.MySqlConnectionString);
    private readonly ILogger<PortfoliosWebContext>? _logger = logger;

    public MySqlConnection GetConnection()
    {
        if (_connection.State != ConnectionState.Open)
        {
            _logger?.LogInformation("Opening connection to database");
            _connection.Open();
        }
        return _connection;
    }
}