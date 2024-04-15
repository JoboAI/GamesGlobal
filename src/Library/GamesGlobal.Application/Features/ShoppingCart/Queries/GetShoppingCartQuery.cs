using GamesGlobal.Application.Features.ShoppingCart.Responses;
using GamesGlobal.Common.Wrappers;
using MediatR;

namespace GamesGlobal.Application.Features.ShoppingCart.Queries;

public class GetShoppingCartQuery : IRequest<IResult<GetShoppingCartQueryResponse>>
{
}