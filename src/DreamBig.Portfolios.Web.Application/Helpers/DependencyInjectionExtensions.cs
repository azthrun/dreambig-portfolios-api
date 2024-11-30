using Microsoft.Extensions.DependencyInjection;

namespace DreamBig.Portfolios.Web.Application.Helpers;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });
        
        return services;
    }
}