using DreamBig.Portfolios.Web.Application.Abstractions;
using DreamBig.Portfolios.Web.Domain.Models;
using DreamBig.Portfolios.Web.Persistent.MySQL.Contexts;
using DreamBig.Portfolios.Web.Persistent.MySQL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DreamBig.Portfolios.Web.Persistent.MySQL.Helpers;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistentLayer(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION") ?? throw new Exception("MYSQL_CONNECTION environment variable not set");
        services.AddSingleton(new Secrets { MySqlConnectionString = connectionString });
        
        services.AddScoped<PortfoliosWebContext>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IExperienceRepository, ExperienceRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();

        return services;
    }
}