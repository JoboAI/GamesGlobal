using GamesGlobal.Core.Entities.ProductSpecificationAttributes;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;

namespace GamesGlobal.Application.Validators.Interfaces;

public interface IAttributeValidator
{
    Task<IEnumerable<string>> ValidateAsync(
        IShoppingCartItemAttributeDomainModel attribute,
        IProductSpecificationAttributeDomainModel productAttribute);
}