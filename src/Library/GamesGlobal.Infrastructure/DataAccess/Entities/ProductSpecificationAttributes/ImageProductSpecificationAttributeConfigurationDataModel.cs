using GamesGlobal.Enum;

namespace GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;

/// <summary>
///     Configuration data for product specification attributes that use an image.
/// </summary>
public class
    ImageProductSpecificationAttributeConfigurationDataModel : BaseProductSpecificationAttributeConfigurationDataModel
{
    /// <summary>
    ///     The identifier for the associated image entity.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    ///     Overridden type property, set to designate an Image attribute.
    /// </summary>
    public override ProductSpecificationAttributeType AttributeType { get; set; } =
        ProductSpecificationAttributeType.Image;
}