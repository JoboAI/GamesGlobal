using GamesGlobal.Enum;

namespace GamesGlobal.Core.Entities.ShoppingCartItemAttribute;

/// <summary>
///     An attribute specifying an image associated with the shopping cart item.
/// </summary>
public class ImageShoppingCartItemAttributeDomainModel : BaseShoppingCartItemAttributeDomainModel
{
    /// <summary>
    ///     Identifier for the related image entity.
    /// </summary>
    public Guid ImageId { get; set; }

    /// <summary>
    ///     Concrete attribute type, set to Image.
    /// </summary>
    public override ProductSpecificationAttributeType AttributeType { get; set; } =
        ProductSpecificationAttributeType.Image;
}