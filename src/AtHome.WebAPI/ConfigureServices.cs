using AtHome.Shared.Interfaces;
using AtHome.WebApi.Database;
using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Repositories;
using AtHome.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AtHome.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Postgres"));
        });
        return services;
    }
    
    public static WebApplicationBuilder ConfigureAspireDbContext(this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<ApplicationDbContext>("athome");
        return builder;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IPlaceService, PlaceService>();
        services.AddScoped<IItemTypeService, ItemTypeService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IPlaceRepository, PlaceRepository>();
        services.AddScoped<IItemTypeRepository, ItemTypeRepository>();
        return services;
    }

    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authority = configuration["Auth:Authority"] ?? "";
        var audience = configuration["Auth:Audience"] ?? "";
        

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = authority;
            options.Audience = audience;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = authority,
                ValidAudience = audience
            };
        });

        services.AddAuthorization();

        return services;
    }
}