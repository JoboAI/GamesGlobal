using GamesGlobal.Infrastructure.DataAccess.Entities.Common;

namespace GamesGlobal.Infrastructure.DataAccess.Entities;

public class ImageDataModel : BaseEntity
{
    /// <summary>
    ///     Binary data of the image.
    /// </summary>
    public byte[] ImageData { get; set; }

    /// <summary>
    ///     MIME type of the image.
    /// </summary>
    public string ContentType { get; set; }
}