using System.Reflection;
using AtHome.Shared.Attributes;
using AtHome.Shared.Handler;
using AtHome.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AtHome.Shared;

public static class ConfigureServices
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClients(configuration);
        
        return services;
    }

    private static IServiceCollection AddRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<HttpAuthenticationHandler>();
        services.AddSingleton<ApiTokenService>();

        var assembly = Assembly.GetExecutingAssembly();

        var refitClients = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<CustomRefitClient>() is not null);

        foreach (var clientType in refitClients)
        {
            services.AddRefitClient(clientType)
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(configuration["WebApi"]!))
                .AddHttpMessageHandler<HttpAuthenticationHandler>();
        }
        
        return services;
    }
}