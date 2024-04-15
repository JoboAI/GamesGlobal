using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Entities.ProductSpecificationAttributes;
using GamesGlobal.Infrastructure.DataAccess.Entities;

namespace GamesGlobal.Test.Shared.Factories;

public static class ProductTestDataFactory
{
    public static ProductDomainModel CreateProductDomainModel(
        string name = "Test Product",
        decimal price = 100.99M,
        string description = "Test Product Description",
        Guid? imageId = null,
        List<IProductSpecificationAttributeDomainModel> specificationAttributes = null
    )
    {
        return new ProductDomainModel
        {
            Name = name,
            Price = price,
            Description = description,
            ImageId = imageId ?? Guid.NewGuid(),
            SpecificationAttributes = specificationAttributes ?? new List<IProductSpecificationAttributeDomainModel>()
        };
    }

    public static ProductDataModel CreateProductDataModel(
        string name = "Test Product",
        decimal price = 100.99M,
        string description = "Test Product Description",
        Guid? imageId = null,
        List<ProductSpecificationAttributeDataModel> specificationAttributes = null
    )
    {
        return new ProductDataModel
        {
            Name = name,
            Price = price,
            Description = description,
            ImageId = imageId ?? Guid.NewGuid(),
            SpecificationAttributes = specificationAttributes ?? new List<ProductSpecificationAttributeDataModel>()
        };
    }
}