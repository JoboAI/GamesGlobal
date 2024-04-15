using GamesGlobal.Application.Features.Image.Handlers;
using GamesGlobal.Application.Features.Image.Queries;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Interfaces.Repositories;
using Moq;
using Xunit;

namespace GamesGlobal.UnitTest.Handlers.Image;

public class GetImageByIdQueryHandlerTests
{
    private readonly GetImageByIdQueryHandler _handler;
    private readonly Mock<IImageRepository> _imageRepositoryMock;

    public GetImageByIdQueryHandlerTests()
    {
        _imageRepositoryMock = new Mock<IImageRepository>();
        _handler = new GetImageByIdQueryHandler(_imageRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ImageFound_ReturnsImageResponse()
    {
        // Arrange
        var imageId = Guid.NewGuid();
        var imageData = new byte[] { 1, 2, 3, 4, 5 }; // Sample image data
        var query = new GetImageByIdQuery(imageId);
        var imageEntity = new ImageDomainModel
        {
            Id = imageId,
            ContentType = "image/png",
            ImageData = imageData
        };

        _imageRepositoryMock.Setup(repo => repo.GetByIdAsync(imageId))
            .ReturnsAsync(imageEntity);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal(imageData, result.Data.ImageData);
        Assert.Equal("image/png", result.Data.ContentType);
        Assert.Equal("Image retrieved successfully.", result.Messages.FirstOrDefault());

        _imageRepositoryMock.Verify(repo => repo.GetByIdAsync(imageId), Times.Once);
    }

    [Fact]
    public async Task Handle_ImageNotFound_ReturnsNotFoundResult()
    {
        // Arrange
        var imageId = Guid.NewGuid();
        var query = new GetImageByIdQuery(imageId);

        _imageRepositoryMock.Setup(repo => repo.GetByIdAsync(imageId))!
            .ReturnsAsync(value: null); // Simulate that the image does not exist

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("Image not found.", result.Messages.FirstOrDefault());

        _imageRepositoryMock.Verify(repo => repo.GetByIdAsync(imageId), Times.Once);
    }
}