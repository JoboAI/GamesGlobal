using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Core.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using IResult = GamesGlobal.Common.Wrappers.IResult;

namespace GamesGlobal.Application.Features.ShoppingCart.Handlers;

public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, IResult>
{
    private readonly ILogger<ClearCartCommandHandler> _logger;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUserContext _userContext;

    public ClearCartCommandHandler(
        IShoppingCartRepository shoppingCartRepository,
        ILogger<ClearCartCommandHandler> logger, IUserContext userContext)
    {
        _shoppingCartRepository =
            shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }

    public async Task<IResult> Handle(ClearCartCommand command, CancellationToken cancellationToken)
    {
        // Clear the shopping cart for the user
        var result = await _shoppingCartRepository.ClearCartAsync(command.ShoppingCartId);
        if (!result)
        {
            _logger.LogError("Unable to clear the cart for user {ShoppingCartId}", command.ShoppingCartId);
            return await Result.FailAsync("Unable to clear the cart.");
        }

        _logger.LogInformation("Cart for user {ShoppingCartId} cleared successfully", command.ShoppingCartId);

        return await Result.SuccessAsync("Cart cleared successfully.");
    }
}