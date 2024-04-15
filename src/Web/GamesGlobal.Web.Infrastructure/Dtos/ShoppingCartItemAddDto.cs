using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;

namespace GamesGlobal.Web.Infrastructure.Dtos;

/// <summary>
///     Represents the data transfer object for adding an item to the shopping cart.
/// </summary>
public class ShoppingCartItemAddDto
{
    /// <summary>
    ///     Gets or sets the unique identifier for the product associated with the shopping cart item.
    /// </summary>
    /// <value>The product ID.</value>
    public Guid ProductId { get; set; }

    /// <summary>
    ///     Gets or sets the quantity of the product to be added to the shopping cart item.
    /// </summary>
    /// <value>The quantity of the product.</value>
    public int Quantity { get; set; }

    /// <summary>
    ///     Gets or sets the collection of attributes for the product variant being added to the shopping cart.
    /// </summary>
    /// <value>A list of product variant attributes.</value>
    public List<IShoppingCartItemAttributeDomainModel> Attributes { get; set; }
}