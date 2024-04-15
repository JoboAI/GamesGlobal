using GamesGlobal.Application.Features.Image.Commands;
using GamesGlobal.Application.Features.Image.Handlers;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Interfaces.Repositories;
using Moq;
using Xunit;

namespace GamesGlobal.UnitTest.Handlers.Image;

public class UploadImageCommandHandlerTests
{
    private readonly UploadImageCommandHandler _handler;
    private readonly Mock<IImageRepository> _imageRepositoryMock;

    public UploadImageCommandHandlerTests()
    {
        _imageRepositoryMock = new Mock<IImageRepository>();
        _handler = new UploadImageCommandHandler(_imageRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommandWithImage_StoresImageSuccessfully()
    {
        // Arrange
        var imageId = Guid.NewGuid();
        var imageStream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5 }); // Replace with actual image data
        var command = new UploadImageCommand
        {
            ImageStream = imageStream,
            ContentType = "image/png"
        };

        var imageEntity = new ImageDomainModel { Id = imageId };

        _imageRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ImageDomainModel>()))
            .ReturnsAsync(imageId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal(imageId, result.Data);
        Assert.Equal("Image uploaded successfully.", result.Messages.FirstOrDefault());

        _imageRepositoryMock.Verify(repo =>
            repo.AddAsync(It.Is<ImageDomainModel>(i =>
                i.ContentType == command.ContentType &&
                i.ImageData.SequenceEqual(new byte[] { 1, 2, 3, 4, 5 }))), Times.Once);

        // Assert that the stream was read
        Assert.Equal(imageStream.Length, imageStream.Position);
    }
}