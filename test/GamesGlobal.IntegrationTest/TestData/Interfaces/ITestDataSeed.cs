using GamesGlobal.Infrastructure.DataAccess;

namespace GamesGlobal.IntegrationTest.TestData.Interfaces;

public interface ITestDataSeed
{
    public int Order { get; }
    Task SeedAsync(GamesGlobalDbContext context);
}