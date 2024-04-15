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

public class UpdateCartItemCommandHandlerTests
{
    private readonly UpdateCartItemCommandHandler _handler;
    private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;
    private readonly Mock<IShoppingCartItemValidator> _shoppingCartItemValidatorMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;

    public UpdateCartItemCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
        _shoppingCartItemValidatorMock = new Mock<IShoppingCartItemValidator>();
        Mock<ILogger<UpdateCartItemCommandHandler>> loggerMock = new();

        _handler = new UpdateCartItemCommandHandler(
            _shoppingCartRepositoryMock.Object,
            loggerMock.Object,
            _shoppingCartItemValidatorMock.Object,
            _productRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_ValidCartItemUpdatesQuantity_ReturnsSuccess()
    {
        // Arrange
        var command = new UpdateCartItemCommand { ItemId = Guid.NewGuid(), Quantity = 2 };
        var existingCartItem = new ShoppingCartItemDomainModel { Id = command.ItemId, Quantity = 1 };

        var product = ProductTestDataFactory.CreateProductDomainModel();
        _productRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        _shoppingCartItemValidatorMock.Setup(repo => repo.ValidateCartItem(It.IsAny<ShoppingCartItemDomainModel>(),
                It.IsAny<ProductDomainModel>()))
            .ReturnsAsync(await Result.SuccessAsync("Success"));

        _shoppingCartRepositoryMock.Setup(repo => repo.GetCartItemByIdAsync(command.ItemId))
            .ReturnsAsync(existingCartItem);

        _shoppingCartRepositoryMock.Setup(repo => repo.UpdateCartItemAsync(existingCartItem))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal("CartItem updated successfully.", result.Messages.FirstOrDefault());
        Assert.Equal(command.Quantity, existingCartItem.Quantity);
        _shoppingCartRepositoryMock.Verify(repo => repo.UpdateCartItemAsync(existingCartItem), Times.Once);
    }

    [Fact]
    public async Task Handle_CartItemNotFound_ReturnsNotFoundResult()
    {
        // Arrange
        var command = new UpdateCartItemCommand { ItemId = Guid.NewGuid(), Quantity = 1 };

        _shoppingCartRepositoryMock.Setup(repo => repo.GetCartItemByIdAsync(command.ItemId))!
            .ReturnsAsync((ShoppingCartItemDomainModel)null); // Simulate that cart item does not exist

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("Cart item not found.", result.Messages.FirstOrDefault());
        _shoppingCartRepositoryMock.Verify(repo => repo.UpdateCartItemAsync(It.IsAny<ShoppingCartItemDomainModel>()),
            Times.Never);
    }
}