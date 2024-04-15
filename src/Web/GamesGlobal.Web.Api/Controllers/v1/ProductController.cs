using AutoMapper;
using GamesGlobal.Application.Features.Product.Queries;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Web.Infrastructure.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using IResult = GamesGlobal.Common.Wrappers.IResult;

namespace GamesGlobal.Web.Api.Controllers.v1;

/// <summary>
///     Controller for managing products.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/products")]
public class ProductController : BaseApiController<ProductController>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ProductController" /> class.
    /// </summary>
    /// <param name="mediator">Mediator instance for sending requests to the Application layer.</param>
    /// <param name="logger">Logger instance to log controller activities.</param>
    /// <param name="mapper">Automapper instance for mapping</param>
    public ProductController(IMediator mediator, ILogger<ProductController> logger, IMapper mapper)
        : base(mediator, logger, mapper)
    {
    }

    /// <summary>
    ///     Retrieves product details by a specified product ID.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to retrieve.</param>
    /// <returns>An action result containing the retrieved product or an error if not found.</returns>
    /// <response code="200">If the product was found and returned successfully.</response>
    /// <response code="404">If the product could not be found.</response>
    /// <response code="500">If there is an internal server error.</response>
    [HttpGet("{productId}")]
    [RequiredScope("products.read")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult<ProductDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IResult))]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        var query = new GetProductByIdQuery(productId);
        var result = await Mediator.Send(query);
        return result.Succeeded ? Ok(result) : NotFound(result);
    }
}