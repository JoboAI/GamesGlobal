using GamesGlobal.Infrastructure.DataAccess.Seed.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.Infrastructure.DataAccess.Seed;

public static class GamesGlobalDbContextSeed
{
    public static async Task SeedAsync(this GamesGlobalDbContext context, IServiceProvider serviceProvider)
    {
        // Retrieve all seeders from the service collection
        var seeders = serviceProvider.GetServices<IDatabaseSeed>()
            .OrderBy(s => s.Order)
            .ToList();

        foreach (var seeder in seeders) await seeder.SeedAsync(context);
    }
}