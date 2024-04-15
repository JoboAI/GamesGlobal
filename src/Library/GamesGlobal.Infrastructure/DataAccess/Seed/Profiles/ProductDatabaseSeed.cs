using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Seed.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Infrastructure.DataAccess.Seed.Profiles;

public class ProductDatabaseSeed : IDatabaseSeed
{
    public static readonly Guid CustomizableTShirtId = new("c1234567-12ab-34cd-56ef-1234567890ab");
    public static readonly Guid HatId = new("d1234567-12ab-34cd-56ef-1234567890ab");
    public int Order => 2;

    public async Task SeedAsync(GamesGlobalDbContext context)
    {
        if (await context.Products.AnyAsync()) return;

        var products = new List<ProductDataModel>
        {
            new()
            {
                Id = CustomizableTShirtId,
                Name = "Customizable T-Shirt",
                Price = 19.99m,
                ImageId = ImageDatabaseSeed.SeededImageId, // Reference to a default placeholder image
                Description = "T-Shirt with your custom design."
            },
            new()
            {
                Id = HatId,
                Name = "Simple Hat",
                Price = 9.99m,
                ImageId = ImageDatabaseSeed.SeededImageId, // Reference to a default placeholder image
                Description = "Add a hat to your order."
            }
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}