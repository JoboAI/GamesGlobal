using GamesGlobal.Infrastructure.DataAccess.Entities.Common;
using GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;

namespace GamesGlobal.Infrastructure.DataAccess.Entities;

/// <summary>
///     Captures specification attributes for a product.
/// </summary>
public class ProductSpecificationAttributeDataModel : BaseEntity
{
    /// <summary>
    ///     The display label for the attribute.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    ///     Flag indicating if the attribute is mandatory.
    /// </summary>
    public bool IsRequired { get; set; }

    public IProductSpecificationAttributeConfigurationDataModel Configuration { get; set; }

    /// <summary>
    ///     Foreign key to the associated product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    ///     Navigation property to the related product.
    /// </summary>
    public virtual ProductDataModel Product { get; set; }
}