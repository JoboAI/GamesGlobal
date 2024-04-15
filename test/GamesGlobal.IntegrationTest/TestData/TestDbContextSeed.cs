using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.IntegrationTest.TestData.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.IntegrationTest.TestData;

public static class TestDbContextSeed
{
    public static async Task SeedTestDataAsync(this GamesGlobalDbContext context, IServiceProvider serviceProvider)
    {
        // Retrieve all seeders from the service collection
        var seeders = serviceProvider
            .GetServices<ITestDataSeed>()
            .OrderBy(s => s.Order)
            .ToList();

        foreach (var seeder in seeders) await seeder.SeedAsync(context);
    }
}