using GamesGlobal.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.UnitTest;

public class InMemoryDatabaseHelper
{
    public static DbContextOptions<GamesGlobalDbContext> CreateNewContextOptions()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .AddEntityFrameworkProxies()
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<GamesGlobalDbContext>();
        builder.UseInMemoryDatabase("TestGamesGlobalDb")
            .UseInternalServiceProvider(serviceProvider);
        return builder.Options;
    }
}