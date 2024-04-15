using GamesGlobal.Enum;

namespace GamesGlobal.Infrastructure.DataAccess.Entities.ShoppingCartItemAttribute;

/// <summary>
///     Base class for different attributes that can be associated with a shopping cart item.
/// </summary>
public abstract class BaseShoppingCartItemAttributeValueDataModel : IShoppingCartItemAttributeValueDataModel
{
    /// <summary>
    ///     Type of the attribute, specified by the derived classes.
    /// </summary>
    public abstract ProductSpecificationAttributeType AttributeType { get; set; }
}