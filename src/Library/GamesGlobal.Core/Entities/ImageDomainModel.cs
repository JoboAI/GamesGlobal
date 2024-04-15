using GamesGlobal.Core.Entities.Common;

namespace GamesGlobal.Core.Entities;

public class ImageDomainModel : BaseDomainModel
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