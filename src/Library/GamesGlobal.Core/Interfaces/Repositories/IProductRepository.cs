using GamesGlobal.Core.Entities;

namespace GamesGlobal.Core.Interfaces.Repositories;

/// <summary>
///     Interface for repository operations related to products.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    ///     Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to retrieve.</param>
    /// <returns>The product domain model that matches the provided ID, or null if the product is not found.</returns>
    Task<ProductDomainModel?> GetByIdAsync(Guid productId);
}