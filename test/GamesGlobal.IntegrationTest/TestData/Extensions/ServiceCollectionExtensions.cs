using GamesGlobal.IntegrationTest.TestData.Interfaces;
using GamesGlobal.IntegrationTest.TestData.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.IntegrationTest.TestData.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection RegisterTestDataSeeders(this IServiceCollection services)
    {
        services.AddScoped<ITestDataSeed, ImageDatabaseSeed>();
        services.AddScoped<ITestDataSeed, ProductDatabaseSeed>();
        services.AddScoped<ITestDataSeed, ProductSpecificationAttributeDatabaseSeed>();
        services.AddScoped<ITestDataSeed, ShoppingCartDatabaseSeed>();
        services.AddScoped<ITestDataSeed, ShoppingCartItemDatabaseSeed>();

        return services;
    }
}