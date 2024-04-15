using GamesGlobal.Application.Features.Image.Commands;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Interfaces.Repositories;
using MediatR;

namespace GamesGlobal.Application.Features.Image.Handlers;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, IResult<Guid>>
{
    private readonly IImageRepository _imageRepository;

    public UploadImageCommandHandler(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task<IResult<Guid>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        // Ensure the stream position is at the beginning before reading
        if (request.ImageStream.CanSeek) request.ImageStream.Position = 0;

        var imageData = new byte[request.ImageStream.Length];
        await request.ImageStream.ReadAsync(imageData.AsMemory(0, imageData.Length), cancellationToken);

        // After successfully reading the data into the array, create the image entity
        var imageEntity = new ImageDomainModel
        {
            ImageData = imageData,
            ContentType = request.ContentType
        };

        // Add the new image entity to the repository
        var result = await _imageRepository.AddAsync(imageEntity);

        // Return success result including the ID of the newly added image
        return await Result<Guid>.SuccessAsync(result, "Image uploaded successfully.");
    }
}