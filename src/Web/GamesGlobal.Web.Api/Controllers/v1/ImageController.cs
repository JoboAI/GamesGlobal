using AutoMapper;
using GamesGlobal.Application.Features.Image.Commands;
using GamesGlobal.Application.Features.Image.Queries;
using GamesGlobal.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using IResult = GamesGlobal.Common.Wrappers.IResult;

namespace GamesGlobal.Web.Api.Controllers.v1;

/// <summary>
///     Controller for image-related operations such as retrieval and upload.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/images")]
public class ImageController : BaseApiController<ImageController>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ImageController" /> class.
    /// </summary>
    /// <param name="mediator">Mediator instance for sending requests to the Application layer.</param>
    /// <param name="logger">Logger instance to log controller activities.</param>
    /// <param name="mapper">Automapper instance for mapping</param>
    public ImageController(IMediator mediator, ILogger<ImageController> logger, IMapper mapper)
        : base(mediator, logger, mapper)
    {
    }

    /// <summary>
    ///     Retrieves an image by its ID and returns it as a file result.
    /// </summary>
    /// <param name="imageId">The unique identifier of the image to retrieve.</param>
    /// <returns>A file result containing the image if found; otherwise, a not-found result.</returns>
    /// <response code="200">Returns the requested image as a file.</response>
    /// <response code="404">Returns a not-found result if the image could not be found.</response>
    [HttpGet("{imageId}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImageById(Guid imageId)
    {
        var query = new GetImageByIdQuery(imageId);
        var result = await Mediator.Send(query);
        if (!result.Succeeded)
        {
            Logger.LogWarning("Image not found with ID: {ImageId}", imageId);
            return NotFound(result);
        }

        return File(result.Data.ImageData, result.Data.ContentType);
    }

    /// <summary>
    ///     Uploads a new image and returns a success or failure result.
    /// </summary>
    /// <param name="file">The image file to upload.</param>
    /// <returns>An action result indicating the success or failure of the image upload operation.</returns>
    /// <response code="200">Returns a success result if the image was successfully uploaded.</response>
    /// <response code="400">Returns a bad-request result if the image could not be uploaded.</response>
    [HttpPost]
    [RequiredScope("images.write")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult<Guid>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
    public async Task<IActionResult> UploadImage(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            Logger.LogWarning("Empty file uploaded");
            return BadRequest("An image file must be provided.");
        }

        using var imageStream = new MemoryStream();
        await file.CopyToAsync(imageStream);

        var command = new UploadImageCommand
        {
            ImageStream = imageStream,
            ContentType = file.ContentType
        };

        var result = await Mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
}