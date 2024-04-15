using GamesGlobal.Common.Wrappers;
using MediatR;

namespace GamesGlobal.Application.Features.Image.Commands;

/// <summary>
///     Represents the command to upload an image.
/// </summary>
public class UploadImageCommand : IRequest<IResult<Guid>>
{
    /// <summary>
    ///     Gets or sets the stream containing the image to be uploaded.
    /// </summary>
    /// <value>The image stream.</value>
    public Stream ImageStream { get; set; }

    /// <summary>
    ///     Gets or sets the content type of the image being uploaded.
    /// </summary>
    /// <value>The MIME content type of the image.</value>
    public string ContentType { get; set; }
}