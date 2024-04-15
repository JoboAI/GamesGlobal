using AutoMapper;
using GamesGlobal.Application.Features.Product.Handlers;
using GamesGlobal.Application.Features.Product.Queries;
using GamesGlobal.Application.Features.Product.Responses;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Interfaces.Repositories;
using Moq;
using Xunit;

namespace GamesGlobal.UnitTest.Handlers.Product;

public class GetProductByIdQueryHandlerTests
{
    private readonly GetProductByIdQueryHandler _handler;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;

    public GetProductByIdQueryHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ProductFound_ReturnsProductResponse()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var query = new GetProductByIdQuery(productId);
        var product = new ProductDomainModel(); // Assuming this matches your domain model
        var productResponse = new ProductResponse(); // Assuming this is your response DTO

        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(product);

        _mapperMock.Setup(mapper => mapper.Map<ProductResponse>(product))
            .Returns(productResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal(productResponse, result.Data);

        _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<ProductResponse>(product), Times.Once);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ReturnsNotFoundResult()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var query = new GetProductByIdQuery(productId);

        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))!
            .ReturnsAsync((ProductDomainModel)null); // Simulate that the product does not exist

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal($"Product with ID {productId} not found.", result.Messages.FirstOrDefault());

        _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<ProductResponse>(It.IsAny<ProductDomainModel>()), Times.Never);
    }
}