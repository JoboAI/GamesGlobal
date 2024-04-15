using GamesGlobal.Common.Wrappers;
using MediatR;

namespace GamesGlobal.Application.Features.ShoppingCart.Commands;

public class ClearCartCommand : IRequest<IResult>
{
    public Guid ShoppingCartId { get; set; }
}