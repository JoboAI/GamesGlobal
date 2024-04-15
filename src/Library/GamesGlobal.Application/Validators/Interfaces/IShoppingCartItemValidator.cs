using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Entities;

namespace GamesGlobal.Application.Validators.Interfaces;

public interface IShoppingCartItemValidator
{
    Task<IResult> ValidateCartItem(ShoppingCartItemDomainModel shoppingCartItem,
        ProductDomainModel product);
}