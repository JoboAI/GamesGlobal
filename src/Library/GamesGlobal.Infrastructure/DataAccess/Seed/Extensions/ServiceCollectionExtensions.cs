using GamesGlobal.Infrastructure.DataAccess.Seed.Interfaces;
using GamesGlobal.Infrastructure.DataAccess.Seed.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.Infrastructure.DataAccess.Seed.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection RegisterDatabaseSeeders(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseSeed, ImageDatabaseSeed>();
        services.AddScoped<IDatabaseSeed, ProductDatabaseSeed>();
        services.AddScoped<IDatabaseSeed, ProductSpecificationAttributeDatabaseSeed>();
        services.AddScoped<IDatabaseSeed, ShoppingCartItemDatabaseSeed>();
        services.AddScoped<IDatabaseSeed, ShoppingCartDatabaseSeed>();

        return services;
    }
}