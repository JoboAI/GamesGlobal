using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Seed.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Infrastructure.DataAccess.Seed.Profiles;

public class ShoppingCartDatabaseSeed : IDatabaseSeed
{
    public static readonly Guid AliceShoppingCartId = new("00000000-0000-0000-0000-000000000001");
    public static readonly Guid BobShoppingCartId = new("00000000-0000-0000-0000-000000000002");
    public static readonly Guid AliceUserIdentifier = new("83dd5b6e-2d57-4d5d-9e6c-efcec513af1a");
    public static readonly Guid BobUserIdentifier = new("00000000-0000-0000-0000-000000000002");

    public int Order => 0; // The order in which this seeder should run, assuming users are needed first

    public async Task SeedAsync(GamesGlobalDbContext context)
    {
        // Check if any users already exist to prevent duplicating data
        if (context.ShoppingCarts.Any()) return;

        // Create an array or list of predefined users with constant Ids
        var shoppingCarts = new[]
        {
            new ShoppingCartDataModel { Id = AliceShoppingCartId, UserIdentifier = AliceUserIdentifier.ToString() },
            new ShoppingCartDataModel { Id = BobShoppingCartId, UserIdentifier = BobUserIdentifier.ToString() }
        };

        foreach (var shoppingCart in shoppingCarts)
            if (await context.ShoppingCarts.FirstOrDefaultAsync(a => a.Id == shoppingCart.Id) == null)
                await context.ShoppingCarts.AddAsync(shoppingCart);

        await context.SaveChangesAsync();
    }
}