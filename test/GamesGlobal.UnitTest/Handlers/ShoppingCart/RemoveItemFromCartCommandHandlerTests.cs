using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Application.Features.ShoppingCart.Handlers;
using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GamesGlobal.UnitTest.Handlers.ShoppingCart;

[Collection("Handler Collection")]
public class RemoveItemFromCartCommandHandlerTests
{
    private readonly RemoveItemFromCartCommandHandler _handler;
    private readonly Mock<ILogger<RemoveItemFromCartCommandHandler>> _loggerMock;
    private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;

    public RemoveItemFromCartCommandHandlerTests()
    {
        _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
        _loggerMock = new Mock<ILogger<RemoveItemFromCartCommandHandler>>();
        _userContextMock = new Mock<IUserContext>();

        _handler = new RemoveItemFromCartCommandHandler(
            _shoppingCartRepositoryMock.Object,
            _loggerMock.Object,
            _userContextMock.Object
        );
    }

    [Fact]
    public async Task Handle_ItemRemoved_ReturnsSuccess()
    {
        // Arrange
        var command = new RemoveItemFromCartCommand
        {
            ShoppingCartId = Guid.NewGuid(),
            ItemId = Guid.NewGuid()
        };
        _shoppingCartRepositoryMock
            .Setup(repo => repo.RemoveItemFromCartAsync(command.ItemId, command.ShoppingCartId))
            .ReturnsAsync(true); // Simulate successful removal

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal("Item removed from the cart successfully.", result.Messages.FirstOrDefault());
        _shoppingCartRepositoryMock.Verify(repo => repo.RemoveItemFromCartAsync(command.ItemId, command.ShoppingCartId),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ItemNotRemoved_ReturnsFailure()
    {
        // Arrange
        var command = new RemoveItemFromCartCommand
        {
            ShoppingCartId = Guid.NewGuid(),
            ItemId = Guid.NewGuid()
        };
        _shoppingCartRepositoryMock
            .Setup(repo => repo.RemoveItemFromCartAsync(command.ItemId, command.ShoppingCartId))
            .ReturnsAsync(false); // Simulate failed removal

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("Error occurred when removing the item from the cart.", result.Messages.FirstOrDefault());
        _shoppingCartRepositoryMock.Verify(repo => repo.RemoveItemFromCartAsync(command.ItemId, command.ShoppingCartId),
            Times.Once);
    }
}