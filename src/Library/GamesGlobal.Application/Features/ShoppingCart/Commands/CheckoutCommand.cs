using GamesGlobal.Common.Wrappers;
using MediatR;

namespace GamesGlobal.Application.Features.ShoppingCart.Commands;

public class CheckoutCommand : IRequest<IResult>
{
    public Guid ShoppingCartId { get; set; }
}