using System.Text.Json.Serialization;
using GamesGlobal.Core.JsonConverters;
using GamesGlobal.Enum;

namespace GamesGlobal.Core.Entities.ShoppingCartItemAttribute;

[JsonConverter(typeof(ShoppingCartItemAttributeConverter))]
public interface IShoppingCartItemAttributeDomainModel
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Foreign key to the associated product specification attribute.
    /// </summary>
    public Guid ProductSpecificationAttributeId { get; set; }

    /// <summary>
    ///     Type of the attribute, specified by the derived classes.
    /// </summary>
    ProductSpecificationAttributeType AttributeType { get; set; }
}