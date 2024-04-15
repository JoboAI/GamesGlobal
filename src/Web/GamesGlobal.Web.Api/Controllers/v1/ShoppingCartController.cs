using AutoMapper;
using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Application.Features.ShoppingCart.Queries;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Web.Infrastructure.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using IResult = GamesGlobal.Common.Wrappers.IResult;

namespace GamesGlobal.Web.Api.Controllers.v1;

/// <summary>
///     Handles shopping cart related operations.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/shopping-cart")]
public class ShoppingCartController : BaseApiController<ShoppingCartController>
{
    /// <summary>
    ///     Initializes a new instance of the ShoppingCartController class.
    /// </summary>
    /// <param name="mediator">Mediator instance for sending requests to the Application layer.</param>
    /// <param name="logger">Logger instance to log controller activities.</param>
    /// <param name="mapper">Automapper instance for mapping</param>
    public ShoppingCartController(IMediator mediator, ILogger<ShoppingCartController> logger, IMapper mapper)
        : base(mediator, logger, mapper)
    {
    }

    /// <summary>
    ///     Retrieves the current user's shopping cart along with items and their attributes.
    /// </summary>
    /// <returns>An action result containing the shopping cart data or an error result.</returns>
    [HttpGet]
    [RequiredScope("shopping-cart.manage")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult<ShoppingCartDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    public async Task<IActionResult> GetCart()
    {
        var query = new GetShoppingCartQuery();
        var result = await Mediator.Send(query);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    ///     Adds an item with specified quantity and attributes to the shopping cart.
    /// </summary>
    /// <param name="shoppingCartId">The unique identifier of the shopping cart.</param>
    /// <param name="model">The data model containing information about the new item to add to the cart.</param>
    /// <returns>An action result indicating success or an error result.</returns>
    [HttpPost("{shoppingCartId}/items")]
    [RequiredScope("shopping-cart.manage")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    public async Task<IActionResult> AddItem(Guid shoppingCartId, ShoppingCartItemAddDto model)
    {
        var command = new AddItemToCartCommand
        {
            ShoppingCartId = shoppingCartId,
            ProductId = model.ProductId,
            Quantity = model.Quantity,
            Attributes = model.Attributes
        };

        var result = await Mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    ///     Updates an existing shopping cart item's quantity and attributes.
    /// </summary>
    /// <param name="shoppingCartId">The unique identifier of the shopping cart.</param>
    /// <param name="itemId">The unique identifier of the shopping cart item to update.</param>
    /// <param name="model">The data model containing the updated quantity and attributes for the cart item.</param>
    /// <returns>An action result indicating success or an error result.</returns>
    [HttpPut("{shoppingCartId}/items/{itemId}")]
    [RequiredScope("shopping-cart.manage")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    public async Task<IActionResult> UpdateItem(Guid shoppingCartId, Guid itemId, ShoppingCartItemUpdateDto model)
    {
        var command = new UpdateCartItemCommand
        {
            ShoppingCartId = shoppingCartId,
            ItemId = itemId,
            Quantity = model.Quantity,
            Attributes = model.Attributes
        };

        var result = await Mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    ///     Removes a specific item from the shopping cart.
    /// </summary>
    /// <param name="shoppingCartId">The unique identifier of the shopping cart.</param>
    /// <param name="itemId">The unique identifier of the cart item to remove.</param>
    /// <returns>An action result indicating success or an error result.</returns>
    [HttpDelete("{shoppingCartId}/items/{itemId}")]
    [RequiredScope("shopping-cart.manage")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    public async Task<IActionResult> RemoveItem(Guid shoppingCartId, Guid itemId)
    {
        var command = new RemoveItemFromCartCommand { ShoppingCartId = shoppingCartId, ItemId = itemId };
        var result = await Mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    ///     Initiates the checkout process for the shopping cart.
    /// </summary>
    /// <param name="shoppingCartId">The unique identifier of the shopping cart for which to initiate the checkout process.</param>
    /// <returns>An action result indicating success or an error result.</returns>
    [HttpPost("{shoppingCartId}/checkout")]
    [RequiredScope("shopping-cart.checkout")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    public async Task<IActionResult> Checkout(Guid shoppingCartId)
    {
        var command = new CheckoutCommand { ShoppingCartId = shoppingCartId };
        var result = await Mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
}