using System.Net;
using System.Text.Json;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.IntegrationTest.Fixtures;
using GamesGlobal.IntegrationTest.Shared;
using GamesGlobal.IntegrationTest.TestData.Profiles;
using GamesGlobal.Web.Api;
using GamesGlobal.Web.Infrastructure.Dtos;
using Xunit;

namespace GamesGlobal.IntegrationTest.IsolatedTests;

public class ProductControllerTests : ApiTestBase
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ProductControllerTests(GamesGlobalWebApplicationFactory<Program> webApplicationFactory) : base(
        webApplicationFactory)
    {
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    [Fact]
    public async Task GetProductById_ReturnsProductDetails_WhenProductExists()
    {
        // Arrange
        var productId = ProductDatabaseSeed.HatId;

        // Act
        var response = await Client.GetAsync($"api/v1/products/{productId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<Result<ProductDto>>(content, _jsonSerializerOptions);

        Assert.True(apiResponse is { Succeeded: true });
        Assert.NotNull(apiResponse?.Data);

        // Further assertions to verify the returned product details
        var productDto = apiResponse.Data;
        Assert.Equal(productId, productDto.Id);

        Assert.NotEmpty(productDto.SpecificationAttributes);
    }

    [Fact]
    public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid(); // Use a non-existing product ID

        // Act
        var response = await Client.GetAsync($"api/v1/products/{productId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<Result<ProductDto>>(content, _jsonSerializerOptions);

        Assert.False(apiResponse.Succeeded);
        Assert.Null(apiResponse.Data);
    }
}