using GamesGlobal.Core.Entities;

namespace GamesGlobal.Core.Interfaces.Repositories;

/// <summary>
///     Interface for repository operations related to shopping carts.
/// </summary>
public interface IShoppingCartRepository
{
    /// <summary>
    ///     Retrieves a shopping cart item by its unique identifier.
    /// </summary>
    /// <param name="itemId">The unique identifier of the shopping cart item to retrieve.</param>
    /// <returns>The shopping cart item domain model that matches the provided ID, or null if not found.</returns>
    Task<ShoppingCartItemDomainModel?> GetCartItemByIdAsync(Guid itemId);

    /// <summary>
    ///     Adds a new item to the specified shopping cart.
    /// </summary>
    /// <param name="shoppingCartId">The unique identifier of the shopping cart.</param>
    /// <param name="item">The shopping cart item domain model to be added.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddItemToCartAsync(Guid shoppingCartId, ShoppingCartItemDomainModel item);

    /// <summary>
    ///     Updates an existing shopping cart item.
    /// </summary>
    /// <param name="cartItem">The shopping cart item domain model with updated information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateCartItemAsync(ShoppingCartItemDomainModel cartItem);

    /// <summary>
    ///     Removes an item from the specified shopping cart.
    /// </summary>
    /// <param name="itemId">The unique identifier of the shopping cart item to remove.</param>
    /// <param name="shoppingCartId">The unique identifier of the shopping cart.</param>
    /// <returns>True if the removal was successful, false otherwise.</returns>
    Task<bool> RemoveItemFromCartAsync(Guid itemId, Guid shoppingCartId);

    /// <summary>
    ///     Clears all items from a specified shopping cart.
    /// </summary>
    /// <param name="shoppingCartId">The unique identifier of the shopping cart to clear.</param>
    /// <returns>True if the cart was successfully cleared, false otherwise.</returns>
    Task<bool> ClearCartAsync(Guid shoppingCartId);

    /// <summary>
    ///     Retrieves an existing shopping cart for the user or creates a new one if it does not exist.
    /// </summary>
    /// <param name="userIdentifier">A string that uniquely identifies the user, typically a user ID or username.</param>
    /// <returns>The shopping cart domain model for the identified user.</returns>
    Task<ShoppingCartDomainModel> GetOrCreateShoppingCartAsync(string userIdentifier);
}