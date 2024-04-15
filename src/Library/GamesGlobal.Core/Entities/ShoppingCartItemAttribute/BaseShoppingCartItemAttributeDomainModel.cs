using GamesGlobal.Core.Entities.Common;
using GamesGlobal.Enum;

namespace GamesGlobal.Core.Entities.ShoppingCartItemAttribute;

/// <summary>
///     Base class for different attributes that can be associated with a shopping cart item.
/// </summary>
public abstract class BaseShoppingCartItemAttributeDomainModel : BaseDomainModel, IShoppingCartItemAttributeDomainModel
{
    /// <summary>
    ///     Foreign key to the associated product specification attribute.
    /// </summary>
    public Guid ProductSpecificationAttributeId { get; set; }

    /// <summary>
    ///     Type of the attribute, specified by the derived classes.
    /// </summary>
    public abstract ProductSpecificationAttributeType AttributeType { get; set; }
}