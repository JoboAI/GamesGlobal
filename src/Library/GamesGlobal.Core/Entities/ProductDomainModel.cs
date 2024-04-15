using GamesGlobal.Core.Entities.Common;
using GamesGlobal.Core.Entities.ProductSpecificationAttributes;

namespace GamesGlobal.Core.Entities;

public class ProductDomainModel : BaseDomainModel
{
    /// <summary>
    ///     Name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Detailed description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Sales price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Identifier for the associated image.
    /// </summary>
    public Guid ImageId { get; set; }

    /// <summary>
    ///     Collection of specification attributes defining product characteristics.
    /// </summary>
    public List<IProductSpecificationAttributeDomainModel> SpecificationAttributes { get; set; }
}