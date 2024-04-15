using GamesGlobal.Application.Features.Image.Queries;
using GamesGlobal.Application.Features.Image.Responses;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Interfaces.Repositories;
using MediatR;

namespace GamesGlobal.Application.Features.Image.Handlers;

public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQuery, IResult<GetImageByIdResponse>>
{
    private readonly IImageRepository _imageRepository;

    public GetImageByIdQueryHandler(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task<IResult<GetImageByIdResponse>> Handle(GetImageByIdQuery request,
        CancellationToken cancellationToken)
    {
        var image = await _imageRepository.GetByIdAsync(request.ImageId);

        if (image == null) return await Result<GetImageByIdResponse>.FailAsync("Image not found.");

        return await Result<GetImageByIdResponse>.SuccessAsync(new GetImageByIdResponse
        {
            ContentType = image.ContentType,
            ImageData = image.ImageData
        }, "Image retrieved successfully.");
    }
}