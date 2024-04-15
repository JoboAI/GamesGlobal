using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;
using MediatR;

namespace GamesGlobal.Application.Features.ShoppingCart.Commands;

public class AddItemToCartCommand : IRequest<IResult>
{
    public Guid ShoppingCartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public List<IShoppingCartItemAttributeDomainModel> Attributes { get; set; } = new();
}