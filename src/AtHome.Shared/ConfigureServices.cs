using Microsoft.Extensions.DependencyInjection;

namespace AtHome.Shared;

public static class ConfigureServices
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<HttpClient>();
        return services;
    }
}