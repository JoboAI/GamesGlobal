using GamesGlobal.Core.Entities.Common;
using GamesGlobal.Enum;

namespace GamesGlobal.Core.Entities.ProductSpecificationAttributes;

/// <summary>
///     Base class for product specification attribute configurations, with an abstract type property.
/// </summary>
public abstract class
    BaseProductSpecificationAttributeDomainModel : BaseDomainModel, IProductSpecificationAttributeDomainModel
{
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
    public abstract ProductSpecificationAttributeType AttributeType { get; set; }
}