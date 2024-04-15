using AutoMapper;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Entities.ProductSpecificationAttributes;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;
using GamesGlobal.Test.Shared.Factories;
using GamesGlobal.UnitTest.Fixtures;
using Xunit;

namespace GamesGlobal.UnitTest.Mappings;

[Collection("AutoMapper Collection")]
public class ProductMappingTests
{
    private readonly IMapper _mapper;

    public ProductMappingTests(AutoMapperFixture fixture)
    {
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void ProductDataModel_To_ProductDomainModel_Mapping_IsValid()
    {
        var productSpecificationAttributes = new List<ProductSpecificationAttributeDataModel>
        {
            new()
            {
                Id = Guid.NewGuid(),
                IsRequired = true,
                Label = "Image",
                Configuration = new ImageProductSpecificationAttributeConfigurationDataModel
                {
                    ContentType = "test"
                }
            }
        };

        // Arrange
        var productData =
            ProductTestDataFactory.CreateProductDataModel(specificationAttributes: productSpecificationAttributes);

        // Act
        var productDomain = _mapper.Map<ProductDomainModel>(productData);

        // Assert
        Assert.NotNull(productDomain);
        Assert.Equal(productData.Name, productDomain.Name);
        Assert.Equal(productData.Description, productDomain.Description);
        Assert.Equal(productData.Price, productDomain.Price);
        Assert.Equal(productData.ImageId, productDomain.ImageId);
    }

    [Fact]
    public void ProductDomainModel_To_ProductDataModel_Mapping_IsValid()
    {
        var productSpecificationAttributes = new List<IProductSpecificationAttributeDomainModel>
        {
            new ImageProductSpecificationAttributeDomainModel
            {
                Id = Guid.NewGuid(),
                IsRequired = true,
                Label = "Image",
                ContentType = "test"
            }
        };

        // Arrange
        var productDomain =
            ProductTestDataFactory.CreateProductDomainModel(specificationAttributes: productSpecificationAttributes);

        // Act
        var productData = _mapper.Map<ProductDataModel>(productDomain);

        // Assert
        Assert.NotNull(productData);
        Assert.Equal(productDomain.Name, productData.Name);
        Assert.Equal(productDomain.Description, productData.Description);
        Assert.Equal(productDomain.Price, productData.Price);
        Assert.Equal(productDomain.ImageId, productData.ImageId);
    }
}