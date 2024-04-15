using GamesGlobal.Infrastructure.DataAccess.Entities.Common;

namespace GamesGlobal.Infrastructure.DataAccess.Entities;

public class ProductDataModel : BaseEntity
{
    /// <summary>
    ///     The product's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The product's price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Product's descriptive text.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     The product's associated image.
    /// </summary>
    public Guid ImageId { get; set; }

    /// <summary>
    ///     Links to the product's specification attributes.
    /// </summary>
    public virtual List<ProductSpecificationAttributeDataModel> SpecificationAttributes { get; set; }

    /// <summary>
    ///     Collection of shopping cart items referencing this product.
    /// </summary>
    public virtual List<ShoppingCartItemDataModel> ShoppingCartItems { get; set; }
}