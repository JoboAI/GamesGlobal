using GamesGlobal.Application.Features.Image.Responses;
using GamesGlobal.Common.Wrappers;
using MediatR;

namespace GamesGlobal.Application.Features.Image.Queries;

/// <summary>
///     Query for retrieving an image by its unique identifier.
/// </summary>
public class GetImageByIdQuery : IRequest<IResult<GetImageByIdResponse>>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="GetImageByIdQuery" /> class.
    /// </summary>
    /// <param name="imageId">The unique identifier for requesting an image.</param>
    public GetImageByIdQuery(Guid imageId)
    {
        ImageId = imageId;
    }

    /// <summary>
    ///     Gets or sets the unique identifier of the image being requested.
    /// </summary>
    /// <value>The unique identifier of the image.</value>
    public Guid ImageId { get; set; }
}