namespace GamesGlobal.Infrastructure.DataAccess.Seed.Interfaces;

public interface IDatabaseSeed
{
    public int Order { get; }
    Task SeedAsync(GamesGlobalDbContext context);
}