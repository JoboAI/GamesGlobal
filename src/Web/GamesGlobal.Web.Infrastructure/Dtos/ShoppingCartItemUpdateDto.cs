using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;

namespace GamesGlobal.Web.Infrastructure.Dtos;

/// <summary>
///     Represents the data transfer object for updating a shopping cart item.
/// </summary>
public class ShoppingCartItemUpdateDto
{
    /// <summary>
    ///     Gets or sets the quantity of the product in the shopping cart item.
    /// </summary>
    /// <value>The quantity of the item.</value>
    public int Quantity { get; set; }

    /// <summary>
    ///     Gets or sets the collection of attributes associated with the shopping cart item.
    /// </summary>
    /// <value>A list of attributes.</value>
    public List<IShoppingCartItemAttributeDomainModel> Attributes { get; set; }
}