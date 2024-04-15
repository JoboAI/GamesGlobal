using GamesGlobal.Core.Entities;

namespace GamesGlobal.Core.Interfaces.Repositories;

/// <summary>
///     Interface for repository operations related to images.
/// </summary>
public interface IImageRepository
{
    /// <summary>
    ///     Retrieves an image by its unique identifier.
    /// </summary>
    /// <param name="imageId">The unique identifier of the image to retrieve.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result is the image domain model that matches the
    ///     provided ID, or null if it doesn't exist.
    /// </returns>
    Task<ImageDomainModel> GetByIdAsync(Guid imageId);

    /// <summary>
    ///     Adds a new image to the repository.
    /// </summary>
    /// <param name="image">The image domain model to add to the repository.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result is the unique identifier (GUID) of the
    ///     newly added image.
    /// </returns>
    Task<Guid> AddAsync(ImageDomainModel image);

    /// <summary>
    ///     Retrieves metadata for an image by its unique identifier.
    /// </summary>
    /// <param name="imageId">The unique identifier of the image for which to retrieve metadata.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result is the image metadata domain model that
    ///     matches the provided ID, or null if it doesn't exist.
    /// </returns>
    Task<ImageMetaDomainModel?> GetImageMetadataAsync(Guid imageId);
}