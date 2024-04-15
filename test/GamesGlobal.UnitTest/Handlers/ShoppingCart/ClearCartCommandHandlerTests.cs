using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Application.Features.ShoppingCart.Handlers;
using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GamesGlobal.UnitTest.Handlers.ShoppingCart;

[Collection("Handler Collection")]
public class ClearCartCommandHandlerTests
{
    private readonly ClearCartCommandHandler _handler;
    private readonly Mock<ILogger<ClearCartCommandHandler>> _loggerMock;
    private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;

    public ClearCartCommandHandlerTests()
    {
        _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
        _loggerMock = new Mock<ILogger<ClearCartCommandHandler>>();
        _userContextMock = new Mock<IUserContext>();

        _handler = new ClearCartCommandHandler(
            _shoppingCartRepositoryMock.Object,
            _loggerMock.Object,
            _userContextMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShoppingCartCleared_ReturnsSuccess()
    {
        // Arrange
        var command = new ClearCartCommand { ShoppingCartId = Guid.NewGuid() };
        _shoppingCartRepositoryMock
            .Setup(repo => repo.ClearCartAsync(command.ShoppingCartId))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal("Cart cleared successfully.", result.Messages.FirstOrDefault());
        _shoppingCartRepositoryMock.Verify(repo => repo.ClearCartAsync(command.ShoppingCartId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShoppingCartNotCleared_ReturnsFailure()
    {
        // Arrange
        var command = new ClearCartCommand { ShoppingCartId = Guid.NewGuid() };
        _shoppingCartRepositoryMock
            .Setup(repo => repo.ClearCartAsync(command.ShoppingCartId))
            .ReturnsAsync(false); // Simulate failure to clear cart

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("Unable to clear the cart.", result.Messages.FirstOrDefault());
        _shoppingCartRepositoryMock.Verify(repo => repo.ClearCartAsync(command.ShoppingCartId), Times.Once);
    }
}