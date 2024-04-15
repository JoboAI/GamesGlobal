using System.Net;
using System.Text;
using System.Text.Json;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;
using GamesGlobal.IntegrationTest.Fixtures;
using GamesGlobal.IntegrationTest.Shared;
using GamesGlobal.IntegrationTest.TestData.Profiles;
using GamesGlobal.Web.Api;
using GamesGlobal.Web.Infrastructure.Dtos;
using Xunit;

namespace GamesGlobal.IntegrationTest.IsolatedTests;

public class ShoppingCartControllerTests : ApiTestBase
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ShoppingCartControllerTests(GamesGlobalWebApplicationFactory<Program> webApplicationFactory) : base(
        webApplicationFactory)
    {
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    [Fact]
    public async Task GetCart_ReturnsCurrentUsersShoppingCart()
    {
        // Act
        var response = await Client.GetAsync("api/v1/shopping-cart");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<Result<ShoppingCartDto>>(content, _jsonSerializerOptions);

        Assert.True(apiResponse is { Succeeded: true });
        Assert.Empty(apiResponse.Messages);

        // Further verify based on your models and expected data:
        var shoppingCartDto = apiResponse.Data;
        Assert.NotEmpty(shoppingCartDto.Items);
    }

    [Fact]
    public async Task AddItem_AddsNewItemToShoppingCart()
    {
        // Arrange
        var shoppingCartId = ShoppingCartDatabaseSeed.AliceShoppingCartId;
        var addItemDto = new ShoppingCartItemAddDto
        {
            ProductId = ProductDatabaseSeed.CustomizableTShirtId,
            Quantity = 1,
            Attributes = new List<IShoppingCartItemAttributeDomainModel>
            {
                new ImageShoppingCartItemAttributeDomainModel
                {
                    Id = Guid.Empty,
                    ProductSpecificationAttributeId = ProductSpecificationAttributeDatabaseSeed.CustomDesignAttributeId,
                    ImageId = ImageDatabaseSeed.SeededImageId
                }
            }
        };

        var json = JsonSerializer.Serialize(addItemDto, _jsonSerializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync($"api/v1/shopping-cart/{shoppingCartId}/items", content);

        var respContent = await response.Content.ReadAsStringAsync();
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<Result>(responseContent, _jsonSerializerOptions);

        Assert.True(apiResponse is { Succeeded: true });
    }
    
    
    [Fact]
    public async Task AddItem_AddsNewItemToShoppingCartWithMissingRequiredAttribute()
    {
        // Arrange
        var shoppingCartId = ShoppingCartDatabaseSeed.AliceShoppingCartId;
        var addItemDto = new ShoppingCartItemAddDto
        {
            ProductId = ProductDatabaseSeed.CustomizableTShirtId,
            Quantity = 1,
            Attributes = new List<IShoppingCartItemAttributeDomainModel>()
        };

        var json = JsonSerializer.Serialize(addItemDto, _jsonSerializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync($"api/v1/shopping-cart/{shoppingCartId}/items", content);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<Result>(responseContent, _jsonSerializerOptions);

        Assert.True(apiResponse is { Succeeded: false });
    }

    [Fact]
    public async Task UpdateItem_UpdatesExistingItemInShoppingCart()
    {
        // Arrange
        var shoppingCartId = ShoppingCartDatabaseSeed.AliceShoppingCartId;
        var itemId = ShoppingCartItemDatabaseSeed.CustomizableTShirtShoppingCartItemId;
        var updateItemDto = new ShoppingCartItemUpdateDto
        {
            Quantity = 2,
            Attributes = new List<IShoppingCartItemAttributeDomainModel>
            {
                new ImageShoppingCartItemAttributeDomainModel
                {
                    Id = Guid.Empty,
                    ProductSpecificationAttributeId = ProductSpecificationAttributeDatabaseSeed.CustomDesignAttributeId,
                    ImageId = ImageDatabaseSeed.SeededImageId
                }
            }
        };

        var json = JsonSerializer.Serialize(updateItemDto, _jsonSerializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PutAsync($"api/v1/shopping-cart/{shoppingCartId}/items/{itemId}", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<Result>(responseContent, _jsonSerializerOptions);

        Assert.True(apiResponse is { Succeeded: true });
    }

    [Fact]
    public async Task RemoveItem_RemovesSpecificItemFromShoppingCart()
    {
        // Arrange
        var shoppingCartId = ShoppingCartDatabaseSeed.AliceShoppingCartId;
        var itemId = ShoppingCartItemDatabaseSeed.HatShoppingCartItemId;

        // Act
        var response = await Client.DeleteAsync($"api/v1/shopping-cart/{shoppingCartId}/items/{itemId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<Result>(responseContent, _jsonSerializerOptions);

        Assert.True(apiResponse is { Succeeded: true });
    }
}