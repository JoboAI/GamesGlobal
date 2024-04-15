using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.IntegrationTest.Fixtures;
using GamesGlobal.IntegrationTest.Shared;
using GamesGlobal.IntegrationTest.TestData.Profiles;
using GamesGlobal.Web.Api;
using Xunit;

namespace GamesGlobal.IntegrationTest.IsolatedTests;

public class ImageControllerTests : ApiTestBase
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ImageControllerTests(GamesGlobalWebApplicationFactory<Program> webApplicationFactory) : base(
        webApplicationFactory)
    {
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    [Fact]
    public async Task GetImageById_ReturnsImageFile_WhenImageExists()
    {
        // Arrange
        var imageId = ImageDatabaseSeed.SeededImageId;

        // Act
        var response = await Client.GetAsync($"api/v1/images/{imageId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var contentType = response.Content.Headers.ContentType?.ToString();
        Assert.True(contentType!.StartsWith("image/"), "Content-Type should be an image type");

        var imageData = await response.Content.ReadAsByteArrayAsync();
        Assert.NotNull(imageData);
        Assert.NotEmpty(imageData);
    }

    [Fact]
    public async Task GetImageById_ReturnsNotFound_WhenImageDoesNotExist()
    {
        // Arrange
        var imageId = Guid.NewGuid(); // Use a non-existing image ID

        // Act
        var response = await Client.GetAsync($"api/v1/images/{imageId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UploadImage_UploadsNewImage_WhenValidFileIsProvided()
    {
        // Arrange
        var filePath = "TestData/Images/TestImg.jpg";
        await using var fileStream = new FileStream(filePath, FileMode.Open);
        var content = new StreamContent(fileStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

        // Wrap the content in a form to emulate form submission
        var formDataContent = new MultipartFormDataContent
        {
            { content, "file", "test-image.jpg" }
        };

        // Act
        var response = await Client.PostAsync("api/v1/images", formDataContent);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse =
            JsonSerializer.Deserialize<Result<Guid>>(responseContent,
                _jsonSerializerOptions);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Succeeded);
        Assert.NotEqual(apiResponse.Data, Guid.Empty);
    }

    [Fact]
    public async Task UploadImage_ReturnsBadRequest_WhenNoFileIsProvided()
    {
        // Arrange
        var content = new MultipartFormDataContent();

        // Act
        var response = await Client.PostAsync("api/v1/images", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}