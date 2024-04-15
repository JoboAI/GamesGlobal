using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Application.Features.ShoppingCart.Handlers;
using GamesGlobal.Application.Validators.Interfaces;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Interfaces.Repositories;
using GamesGlobal.Test.Shared.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GamesGlobal.UnitTest.Handlers.ShoppingCart;

[Collection("Handler Collection")]
public class AddItemToCartCommandHandlerTests
{
    private readonly AddItemToCartCommandHandler _handler;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;
    private readonly Mock<IShoppingCartItemValidator> _shoppingCartItemValidatorMock;

    public AddItemToCartCommandHandlerTests()
    {
        _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _shoppingCartItemValidatorMock = new Mock<IShoppingCartItemValidator>();

        Mock<ILogger<AddItemToCartCommandHandler>> loggerMock = new();

        _handler = new AddItemToCartCommandHandler(
            _shoppingCartRepositoryMock.Object,
            _productRepositoryMock.Object,
            loggerMock.Object,
            _shoppingCartItemValidatorMock.Object
        );
    }

    [Fact]
    public async Task Handle_GivenValidProductId_AddsItemToCart()
    {
        // Arrange
        var command = new AddItemToCartCommand
        {
            ProductId = Guid.NewGuid(),
            ShoppingCartId = Guid.NewGuid(),
            Quantity = 1
        };
        var product = ProductTestDataFactory.CreateProductDomainModel();
        _productRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.ProductId))
            .ReturnsAsync(product);

        _shoppingCartItemValidatorMock.Setup(repo => repo.ValidateCartItem(It.IsAny<ShoppingCartItemDomainModel>(),
                It.IsAny<ProductDomainModel>()))
            .ReturnsAsync(await Result.SuccessAsync("Success"));

        _shoppingCartRepositoryMock
            .Setup(repo => repo.AddItemToCartAsync(command.ShoppingCartId, It.IsAny<ShoppingCartItemDomainModel>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal("Item added to the cart successfully.", result.Messages.FirstOrDefault());
        _shoppingCartRepositoryMock.Verify(repo =>
            repo.AddItemToCartAsync(command.ShoppingCartId, It.IsAny<ShoppingCartItemDomainModel>()), Times.Once);
    }

    [Fact]
    public async Task Handle_GivenInvalidProductId_ReturnsNotFound()
    {
        // Arrange
        var command = new AddItemToCartCommand
        {
            ProductId = Guid.NewGuid(),
            ShoppingCartId = Guid.NewGuid(),
            Quantity = 1
        };
        _productRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.ProductId))!
            .ReturnsAsync(value: null); // Simulate that the product does not exist

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Contains("Product not found.", result.Messages);
        _shoppingCartRepositoryMock.Verify(repo =>
            repo.AddItemToCartAsync(It.IsAny<Guid>(), It.IsAny<ShoppingCartItemDomainModel>()), Times.Never);
    }
}