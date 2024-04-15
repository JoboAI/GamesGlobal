using GamesGlobal.Application.Features.ShoppingCart.Handlers;
using GamesGlobal.Application.Features.ShoppingCart.Queries;
using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Core.Interfaces.Repositories;
using GamesGlobal.Test.Shared.Factories;
using Moq;
using Xunit;

namespace GamesGlobal.UnitTest.Handlers.ShoppingCart;

[Collection("Handler Collection")]
public class GetShoppingCartQueryHandlerTests
{
    private readonly GetShoppingCartQueryHandler _handler;
    private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;

    public GetShoppingCartQueryHandlerTests()
    {
        _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
        _userContextMock = new Mock<IUserContext>();

        _handler = new GetShoppingCartQueryHandler(_shoppingCartRepositoryMock.Object, _userContextMock.Object);
    }

    [Fact]
    public async Task Handle_GetShoppingCart_ReturnsCorrectData()
    {
        // Arrange
        var expectedUserId = "TestUser";
        var expectedShoppingCart = ShoppingCartTestDataFactory.CreateShoppingCartDomainModel();
        var query = new GetShoppingCartQuery();

        _userContextMock
            .Setup(x => x.GetUserId())
            .Returns(expectedUserId);

        _shoppingCartRepositoryMock
            .Setup(x => x.GetOrCreateShoppingCartAsync(expectedUserId))
            .ReturnsAsync(expectedShoppingCart);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal(expectedShoppingCart.Items.Count, result.Data.Items.Count);
        Assert.Equal(expectedShoppingCart.Id, result.Data.Id);
    }
}