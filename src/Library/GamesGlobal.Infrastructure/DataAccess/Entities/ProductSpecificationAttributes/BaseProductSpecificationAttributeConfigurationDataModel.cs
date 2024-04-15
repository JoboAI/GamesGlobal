using GamesGlobal.Enum;

namespace GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;

/// <summary>
///     Base class for product specification attribute configurations, with an abstract type property.
/// </summary>
public abstract class
    BaseProductSpecificationAttributeConfigurationDataModel : IProductSpecificationAttributeConfigurationDataModel
{
    /// <summary>
    ///     The type of specification attribute (abstract, implement in derived classes).
    /// </summary>
    public abstract ProductSpecificationAttributeType AttributeType { get; set; }
}