using System.Text.Json.Serialization;
using GamesGlobal.Core.JsonConverters;
using GamesGlobal.Enum;

namespace GamesGlobal.Core.Entities.ProductSpecificationAttributes;

[JsonConverter(typeof(ProductSpecificationAttributeConverter))]
public interface IProductSpecificationAttributeDomainModel
{
    public Guid Id { get; set; }

    /// <summary>
    ///     The display label for the attribute.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    ///     Flag indicating if the attribute is mandatory.
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    ///     The type of specification attribute (abstract, implement in derived classes).
    /// </summary>
    ProductSpecificationAttributeType AttributeType { get; set; }
}