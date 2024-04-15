using AutoMapper;
using GamesGlobal.Core.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Test.Shared.Factories;
using GamesGlobal.UnitTest.Fixtures;
using Xunit;

namespace GamesGlobal.UnitTest.Mappings;

[Collection("AutoMapper Collection")]
public class ShoppingCartMappingTests
{
    private readonly IMapper _mapper;

    public ShoppingCartMappingTests(AutoMapperFixture fixture)
    {
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void ShoppingCartDataModel_To_ShoppingCartDomainModel_Mapping_IsValid()
    {
        // Arrange
        var shoppingCartData = ShoppingCartTestDataFactory.CreateShoppingCartDataModel();

        // Act
        var shoppingCartDomain = _mapper.Map<ShoppingCartDomainModel>(shoppingCartData);

        // Assert
        Assert.NotNull(shoppingCartDomain);
        Assert.Equal(shoppingCartData.Items.Count, shoppingCartDomain.Items.Count);

        for (var i = 0; i < shoppingCartData.Items.Count; i++)
        {
            Assert.Equal(shoppingCartData.Items[i].ProductId, shoppingCartDomain.Items[i].ProductId);
            Assert.Equal(shoppingCartData.Items[i].Product.Price, shoppingCartDomain.Items[i].UnitPrice);
            Assert.Equal(shoppingCartData.Items[i].Quantity, shoppingCartDomain.Items[i].Quantity);
        }
    }

    [Fact]
    public void ShoppingCartDomainModel_To_ShoppingCartDataModel_Mapping_IsValid()
    {
        // Arrange
        var shoppingCartDomain = ShoppingCartTestDataFactory.CreateShoppingCartDomainModel();

        // Act
        var shoppingCartData = _mapper.Map<ShoppingCartDataModel>(shoppingCartDomain);

        // Assert
        Assert.NotNull(shoppingCartData);
        Assert.Equal(shoppingCartDomain.Items.Count, shoppingCartData.Items.Count);

        for (var i = 0; i < shoppingCartDomain.Items.Count; i++)
        {
            Assert.Equal(shoppingCartDomain.Items[i].ProductId, shoppingCartData.Items[i].ProductId);
            Assert.Equal(shoppingCartDomain.Items[i].Quantity, shoppingCartData.Items[i].Quantity);
        }
    }
}