using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;
using GamesGlobal.IntegrationTest.TestData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.IntegrationTest.TestData.Profiles;

public class ProductSpecificationAttributeDatabaseSeed : ITestDataSeed
{
    public static readonly Guid CustomDesignAttributeId = new("e1234567-12ab-34cd-56ef-1234567890ab");
    public static readonly Guid AddHatPictureAttributeId = new("f1234567-12ab-34cd-56ef-1234567890ab");
    public int Order => 4;

    public async Task SeedAsync(GamesGlobalDbContext context)
    {
        if (await context.ProductSpecificationAttributes.AnyAsync()) return;

        var specificationAttributes = new List<ProductSpecificationAttributeDataModel>
        {
            new()
            {
                Id = CustomDesignAttributeId,
                Label = "Custom Design",
                IsRequired = true,
                ProductId = ProductDatabaseSeed.CustomizableTShirtId,
                Configuration = new ImageProductSpecificationAttributeConfigurationDataModel
                {
                    ContentType = "image/png"
                }
            },
            new()
            {
                Id = AddHatPictureAttributeId,
                Label = "Add Hat Picture",
                IsRequired = true,
                ProductId = ProductDatabaseSeed.HatId,
                Configuration = new ImageProductSpecificationAttributeConfigurationDataModel
                {
                    ContentType = "image/png"
                }
            }
        };

        await context.ProductSpecificationAttributes.AddRangeAsync(specificationAttributes);
        await context.SaveChangesAsync();
    }
}