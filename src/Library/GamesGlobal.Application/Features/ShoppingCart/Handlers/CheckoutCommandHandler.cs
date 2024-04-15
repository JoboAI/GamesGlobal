using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Core.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using IResult = GamesGlobal.Common.Wrappers.IResult;

namespace GamesGlobal.Application.Features.ShoppingCart.Handlers;

public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, IResult>
{
    private readonly ILogger<CheckoutCommandHandler> _logger;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUserContext _userContext;

    public CheckoutCommandHandler(
        IShoppingCartRepository shoppingCartRepository,
        ILogger<CheckoutCommandHandler> logger, IUserContext userContext)
    {
        _shoppingCartRepository =
            shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }

    public async Task<IResult> Handle(CheckoutCommand command, CancellationToken cancellationToken)
    {
        // Perform the checkout operation, which clears the cart for this basic implementation
        var clearResult = await _shoppingCartRepository.ClearCartAsync(command.ShoppingCartId);
        if (!clearResult)
        {
            _logger.LogError("Could not clear the cart for the checkout process for user {ShoppingCartId}",
                command.ShoppingCartId);
            return await Result.FailAsync("Error occurred during the checkout process.");
        }

        _logger.LogInformation("Checkout process completed and cart cleared for user {ShoppingCartId}",
            command.ShoppingCartId);
        return await Result.SuccessAsync("Checkout successful and cart cleared.");
    }
}