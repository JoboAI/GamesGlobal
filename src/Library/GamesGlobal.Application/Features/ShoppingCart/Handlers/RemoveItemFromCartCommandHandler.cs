using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Core.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using IResult = GamesGlobal.Common.Wrappers.IResult;

namespace GamesGlobal.Application.Features.ShoppingCart.Handlers;

public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, IResult>
{
    private readonly ILogger<RemoveItemFromCartCommandHandler> _logger;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUserContext _userContext;

    public RemoveItemFromCartCommandHandler(
        IShoppingCartRepository shoppingCartRepository,
        ILogger<RemoveItemFromCartCommandHandler> logger, IUserContext userContext)
    {
        _shoppingCartRepository =
            shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }

    public async Task<IResult> Handle(RemoveItemFromCartCommand command, CancellationToken cancellationToken)
    {
        // Try to remove the specified cart item
        var removeResult =
            await _shoppingCartRepository.RemoveItemFromCartAsync(command.ItemId, command.ShoppingCartId);
        if (!removeResult)
        {
            _logger.LogError(
                "Could not remove the item with ID {ItemId} from the cart for shopping cart id {ShoppingCartId}",
                command.ItemId, command.ShoppingCartId);
            return await Result.FailAsync("Error occurred when removing the item from the cart.");
        }

        _logger.LogInformation("Removed item with ID {ItemId} from the cart for shopping cart id {ShoppingCartId}",
            command.ItemId, command.ShoppingCartId);
        return await Result.SuccessAsync("Item removed from the cart successfully.");
    }
}