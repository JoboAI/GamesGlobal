using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.IntegrationTest.TestData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.IntegrationTest.TestData.Profiles;

public class ProductDatabaseSeed : ITestDataSeed
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
                ImageId = ImageDatabaseSeed.SeededImageId,
                Description = "T-Shirt with your custom design."
            },
            new()
            {
                Id = HatId,
                Name = "Simple Hat",
                Price = 9.99m,
                ImageId = ImageDatabaseSeed.SeededImageId,
                Description = "Add a hat to your order."
            }
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}