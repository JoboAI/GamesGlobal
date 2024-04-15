using GamesGlobal.Enum;

namespace GamesGlobal.Infrastructure.DataAccess.Entities.ShoppingCartItemAttribute;

/// <summary>
///     An attribute specifying an image associated with the shopping cart item.
/// </summary>
public class ImageShoppingCartItemAttributeValueDataModel : BaseShoppingCartItemAttributeValueDataModel
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