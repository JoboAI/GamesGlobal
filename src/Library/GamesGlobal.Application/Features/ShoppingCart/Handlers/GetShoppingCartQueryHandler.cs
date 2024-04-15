using GamesGlobal.Application.Features.ShoppingCart.Queries;
using GamesGlobal.Application.Features.ShoppingCart.Responses;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Core.Interfaces.Repositories;
using MediatR;

namespace GamesGlobal.Application.Features.ShoppingCart.Handlers;

public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, IResult<GetShoppingCartQueryResponse>>
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUserContext _userContext;

    public GetShoppingCartQueryHandler(
        IShoppingCartRepository shoppingCartRepository,
        IUserContext userContext)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _userContext = userContext;
    }

    public async Task<IResult<GetShoppingCartQueryResponse>> Handle(GetShoppingCartQuery request,
        CancellationToken cancellationToken)
    {
        // Retrieve the user ID from the user context assuming it can get it from the current context
        var userId = _userContext.GetUserId();

        // Retrieve shopping cart items for the user from the repository
        var shoppingCart = await _shoppingCartRepository.GetOrCreateShoppingCartAsync(userId);

        // Prepare the response
        var response = new GetShoppingCartQueryResponse
        {
            Items = shoppingCart.Items,
            Id = shoppingCart.Id
        };

        // Return the successful result
        return await Result<GetShoppingCartQueryResponse>.SuccessAsync(response);
    }
}