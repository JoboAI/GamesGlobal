using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.ShoppingCartItemAttribute;
using GamesGlobal.IntegrationTest.TestData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.IntegrationTest.TestData.Profiles;

public class ShoppingCartItemDatabaseSeed : ITestDataSeed
{
    public static readonly Guid CustomizableTShirtShoppingCartItemId = new("00000000-0000-0000-0000-000000000001");
    public static readonly Guid HatShoppingCartItemId = new("00000000-0000-0000-0000-000000000002");
    public int Order => 3;

    public async Task SeedAsync(GamesGlobalDbContext context)
    {
        if (await context.ShoppingCartItems.AnyAsync()) return;

        var cartId = ShoppingCartDatabaseSeed.AliceShoppingCartId;

        var shoppingCartItems = new List<ShoppingCartItemDataModel>
        {
            new()
            {
                ProductId = ProductDatabaseSeed.CustomizableTShirtId,
                ShoppingCartId = cartId,
                Id = CustomizableTShirtShoppingCartItemId,
                Quantity = 1,
                Attributes = new List<ShoppingCartItemAttributeDataModel>
                {
                    new()
                    {
                        ProductSpecificationAttributeId =
                            ProductSpecificationAttributeDatabaseSeed.CustomDesignAttributeId,
                        Value = new ImageShoppingCartItemAttributeValueDataModel
                        {
                            ImageId = ImageDatabaseSeed.SeededImageId
                        }
                    }
                }
            },
            new()
            {
                ProductId = ProductDatabaseSeed.HatId,
                ShoppingCartId = cartId,
                Quantity = 1,
                Id = HatShoppingCartItemId,
                Attributes = new List<ShoppingCartItemAttributeDataModel>
                {
                    new()
                    {
                        ProductSpecificationAttributeId =
                            ProductSpecificationAttributeDatabaseSeed.AddHatPictureAttributeId,
                        Value = new ImageShoppingCartItemAttributeValueDataModel
                        {
                            ImageId = ImageDatabaseSeed.SeededImageId
                        }
                    }
                }
            }
        };

        await context.ShoppingCartItems.AddRangeAsync(shoppingCartItems);
        await context.SaveChangesAsync();
    }
}