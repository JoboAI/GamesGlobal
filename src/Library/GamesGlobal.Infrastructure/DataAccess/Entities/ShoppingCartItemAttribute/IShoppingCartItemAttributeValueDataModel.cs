using GamesGlobal.Enum;

namespace GamesGlobal.Infrastructure.DataAccess.Entities.ShoppingCartItemAttribute;

public interface IShoppingCartItemAttributeValueDataModel
{
    /// <summary>
    ///     Type of the attribute, specified by the derived classes.
    /// </summary>
    ProductSpecificationAttributeType AttributeType { get; set; }
}