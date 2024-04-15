using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Application.Validators.Interfaces;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GamesGlobal.Application.Features.ShoppingCart.Handlers;

public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, IResult>
{
    private readonly ILogger<UpdateCartItemCommandHandler> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IShoppingCartItemValidator _shoppingCartItemValidator;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public UpdateCartItemCommandHandler(
        IShoppingCartRepository shoppingCartRepository,
        ILogger<UpdateCartItemCommandHandler> logger, IShoppingCartItemValidator shoppingCartItemValidator,
        IProductRepository productRepository)
    {
        _shoppingCartRepository =
            shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _shoppingCartItemValidator = shoppingCartItemValidator ??
                                     throw new ArgumentNullException(nameof(shoppingCartItemValidator));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<IResult> Handle(UpdateCartItemCommand command, CancellationToken cancellationToken)
    {
        // Retrieve the cart item to be updated
        var cartItem = await _shoppingCartRepository.GetCartItemByIdAsync(command.ItemId);

        if (cartItem == null)
        {
            _logger.LogWarning("Shopping cart item with ID {ItemId} not found", command.ItemId);
            return await Result.FailAsync("Cart item not found.");
        }

        // Verify the product exists
        var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
        if (product == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", cartItem.ProductId);
            return await Result.FailAsync("Product not found.");
        }

        // Update the quantity of the cart item
        cartItem.Quantity = command.Quantity;
        cartItem.Attributes = command.Attributes;

        var validationResult = await _shoppingCartItemValidator.ValidateCartItem(cartItem, product);

        if (!validationResult.Succeeded) return await Result.FailAsync(validationResult.Messages);

        // Update the cart item in the repository
        await _shoppingCartRepository.UpdateCartItemAsync(cartItem);

        _logger.LogInformation("Shopping cart item with ID {ItemId} updated successfully", command.ItemId);

        return await Result.SuccessAsync("CartItem updated successfully.");
    }
}