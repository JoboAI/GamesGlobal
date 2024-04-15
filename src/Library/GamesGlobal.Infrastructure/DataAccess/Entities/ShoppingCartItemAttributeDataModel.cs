using GamesGlobal.Infrastructure.DataAccess.Entities.Common;
using GamesGlobal.Infrastructure.DataAccess.Entities.ShoppingCartItemAttribute;

namespace GamesGlobal.Infrastructure.DataAccess.Entities;

/// <summary>
///     Captures specification attributes for a ShoppingCartItem.
/// </summary>
public class ShoppingCartItemAttributeDataModel : BaseEntity
{
    /// <summary>
    ///     Foreign key to the associated product specification attribute.
    /// </summary>
    public Guid ProductSpecificationAttributeId { get; set; }

    /// <summary>
    ///     Navigation property to the related product specification attribute.
    /// </summary>
    public virtual ProductSpecificationAttributeDataModel ProductSpecificationAttribute { get; set; }

    /// <summary>
    ///     Foreign key to the associated shopping cart item.
    /// </summary>
    public Guid ShoppingCartItemId { get; set; }

    /// <summary>
    ///     Navigation property to the related shopping cart item.
    /// </summary>
    public virtual ShoppingCartItemDataModel ShoppingCartItem { get; set; }

    public IShoppingCartItemAttributeValueDataModel Value { get; set; }
}