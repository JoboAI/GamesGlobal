using GamesGlobal.Enum;

namespace GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;

public interface IProductSpecificationAttributeConfigurationDataModel
{
    /// <summary>
    ///     The type of specification attribute (abstract, implement in derived classes).
    /// </summary>
    ProductSpecificationAttributeType AttributeType { get; set; }
}