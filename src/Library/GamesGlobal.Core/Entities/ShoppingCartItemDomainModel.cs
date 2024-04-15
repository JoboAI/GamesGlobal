using GamesGlobal.Core.Entities.Common;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;

namespace GamesGlobal.Core.Entities;

public class ShoppingCartItemDomainModel : BaseDomainModel
{
    /// <summary>
    ///     Identifier for the related product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    ///     Price per unit of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    ///     Number of items of the product.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    ///     List of attributes providing additional customization for the shopping cart item.
    /// </summary>
    public List<IShoppingCartItemAttributeDomainModel> Attributes { get; set; }
}