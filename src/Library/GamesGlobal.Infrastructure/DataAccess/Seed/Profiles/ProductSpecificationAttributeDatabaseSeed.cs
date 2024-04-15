using GamesGlobal.Enum;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;
using GamesGlobal.Infrastructure.DataAccess.Seed.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Infrastructure.DataAccess.Seed.Profiles;

public class ProductSpecificationAttributeDatabaseSeed : IDatabaseSeed
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
                Id = CustomDesignAttributeId, // Predefined static ID
                Label = "Custom Design",
                IsRequired = true,
                ProductId = ProductDatabaseSeed.CustomizableTShirtId,
                Configuration = new ImageProductSpecificationAttributeConfigurationDataModel
                {
                    AttributeType = ProductSpecificationAttributeType.Image,
                    ContentType = "image/png"
                }
            },
            new()
            {
                Id = AddHatPictureAttributeId, // Predefined static ID
                Label = "Add Hat Picture",
                IsRequired = true,
                ProductId = ProductDatabaseSeed.HatId,
                Configuration = new ImageProductSpecificationAttributeConfigurationDataModel
                {
                    AttributeType = ProductSpecificationAttributeType.Image,
                    ContentType = "image/png"
                }
            }
        };

        await context.ProductSpecificationAttributes.AddRangeAsync(specificationAttributes);
        await context.SaveChangesAsync();
    }
}