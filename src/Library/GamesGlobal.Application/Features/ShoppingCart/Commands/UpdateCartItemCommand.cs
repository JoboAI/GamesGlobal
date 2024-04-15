using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;
using MediatR;

namespace GamesGlobal.Application.Features.ShoppingCart.Commands;

public class UpdateCartItemCommand : IRequest<IResult>
{
    public Guid ShoppingCartId { get; set; }
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }

    public List<IShoppingCartItemAttributeDomainModel> Attributes { get; set; }
}