using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Application.Validators.Interfaces;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using IResult = GamesGlobal.Common.Wrappers.IResult;

namespace GamesGlobal.Application.Features.ShoppingCart.Handlers;

public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, IResult>
{
    private readonly ILogger<AddItemToCartCommandHandler> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IShoppingCartItemValidator _shoppingCartItemValidator;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public AddItemToCartCommandHandler(
        IShoppingCartRepository shoppingCartRepository,
        IProductRepository productRepository,
        ILogger<AddItemToCartCommandHandler> logger,
        IShoppingCartItemValidator shoppingCartItemValidator)
    {
        _shoppingCartRepository =
            shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _shoppingCartItemValidator = shoppingCartItemValidator ??
                                     throw new ArgumentNullException(nameof(shoppingCartItemValidator));
    }

    public async Task<IResult> Handle(AddItemToCartCommand command, CancellationToken cancellationToken)
    {
        // Verify the product exists
        var product = await _productRepository.GetByIdAsync(command.ProductId);
        if (product == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", command.ProductId);
            return await Result.FailAsync("Product not found.");
        }

        // Add item to the shopping cart
        var cartItem = new ShoppingCartItemDomainModel
        {
            ProductId = command.ProductId,
            Quantity = command.Quantity,
            UnitPrice = product.Price,
            Attributes = command.Attributes
        };

        var validationResult = await _shoppingCartItemValidator.ValidateCartItem(cartItem, product);

        if (!validationResult.Succeeded) return await Result.FailAsync(validationResult.Messages);

        await _shoppingCartRepository.AddItemToCartAsync(command.ShoppingCartId, cartItem);

        _logger.LogInformation("Product {ProductId} added to the cart of user {ShoppingCartId}", command.ProductId,
            command.ShoppingCartId);

        return await Result.SuccessAsync("Item added to the cart successfully.");
    }
}