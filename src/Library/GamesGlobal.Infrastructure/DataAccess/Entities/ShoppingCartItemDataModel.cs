using System.ComponentModel.DataAnnotations.Schema;
using GamesGlobal.Infrastructure.DataAccess.Entities.Common;

namespace GamesGlobal.Infrastructure.DataAccess.Entities;

/// <summary>
///     Represents an item within a shopping cart.
/// </summary>
public class ShoppingCartItemDataModel : BaseEntity
{
    /// <summary>
    ///     Identifier for the related product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    ///     Number of items of the product.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    ///     Foreign key relating to the user who owns the shopping cart.
    /// </summary>
    public Guid ShoppingCartId { get; set; }

    /// <summary>
    ///     Navigation property to the user entity.
    /// </summary>
    public virtual ShoppingCartDataModel ShoppingCart { get; set; }

    /// <summary>
    ///     List of attributes providing additional customization for the shopping cart item.
    /// </summary>
    public virtual List<ShoppingCartItemAttributeDataModel> Attributes { get; set; }

    /// <summary>
    ///     Navigation property to the associated product.
    /// </summary>
    [ForeignKey("ProductId")]
    public virtual ProductDataModel Product { get; set; }
}