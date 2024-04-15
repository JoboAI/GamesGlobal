using GamesGlobal.Common.Wrappers;
using MediatR;

namespace GamesGlobal.Application.Features.ShoppingCart.Commands;

public class RemoveItemFromCartCommand : IRequest<IResult>
{
    public Guid ShoppingCartId { get; set; }
    public Guid ItemId { get; set; }
}