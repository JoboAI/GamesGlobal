using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.IntegrationTest.TestData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.IntegrationTest.TestData.Profiles;

public class ShoppingCartDatabaseSeed : ITestDataSeed
{
    public static readonly Guid AliceShoppingCartId = new("00000000-0000-0000-0000-000000000001");
    public static readonly Guid BobShoppingCartId = new("00000000-0000-0000-0000-000000000002");
    public static readonly Guid AliceUserIdentifier = new("83dd5b6e-2d57-4d5d-9e6c-efcec513af1a");
    public static readonly Guid BobUserIdentifier = new("00000000-0000-0000-0000-000000000002");

    public int Order => 0;

    public async Task SeedAsync(GamesGlobalDbContext context)
    {
        if (context.ShoppingCarts.Any()) return;

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