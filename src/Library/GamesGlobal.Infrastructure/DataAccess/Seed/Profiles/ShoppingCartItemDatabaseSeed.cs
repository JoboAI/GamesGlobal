using GamesGlobal.Enum;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.ShoppingCartItemAttribute;
using GamesGlobal.Infrastructure.DataAccess.Seed.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Infrastructure.DataAccess.Seed.Profiles;

public class ShoppingCartItemDatabaseSeed : IDatabaseSeed
{
    public int Order => 3;

    public async Task SeedAsync(GamesGlobalDbContext context)
    {
        // Check if shopping cart items already exist to prevent duplicating data
        if (await context.ShoppingCartItems.AnyAsync()) return;

        var cartId = ShoppingCartDatabaseSeed.AliceShoppingCartId;

        var shoppingCartItems = new List<ShoppingCartItemDataModel>
        {
            new() // Add a customizable t-shirt to Alice's cart
            {
                ProductId = ProductDatabaseSeed.CustomizableTShirtId, // Customizable T-Shirt ID
                ShoppingCartId = cartId,
                Quantity = 1,
                Attributes = new List<ShoppingCartItemAttributeDataModel>
                {
                    new()
                    {
                        ProductSpecificationAttributeId =
                            ProductSpecificationAttributeDatabaseSeed.CustomDesignAttributeId,
                        Value = new ImageShoppingCartItemAttributeValueDataModel
                        {
                            AttributeType = ProductSpecificationAttributeType.Image,
                            ImageId = ImageDatabaseSeed.SeededImageId
                        }
                    }
                }
            },
            new() // Add a hat to Alice's cart
            {
                ProductId = ProductDatabaseSeed.HatId, // Hat ID
                ShoppingCartId = cartId,
                Quantity = 1,
                Attributes = new List<ShoppingCartItemAttributeDataModel>
                {
                    new()
                    {
                        ProductSpecificationAttributeId =
                            ProductSpecificationAttributeDatabaseSeed.AddHatPictureAttributeId,
                        Value = new ImageShoppingCartItemAttributeValueDataModel
                        {
                            AttributeType = ProductSpecificationAttributeType.Image,
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