using AutoMapper;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Interfaces.Repositories;
using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly GamesGlobalDbContext _dbContext;
    private readonly IMapper _mapper;

    public ImageRepository(GamesGlobalDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ImageDomainModel> GetByIdAsync(Guid imageId)
    {
        var productEntity = await _dbContext.Images
            .FirstOrDefaultAsync(p => p.Id == imageId);

        return _mapper.Map<ImageDomainModel>(productEntity);
    }

    public async Task<Guid> AddAsync(ImageDomainModel image)
    {
        var imageDataModel = _mapper.Map<ImageDataModel>(image);
        _dbContext.Images.Add(imageDataModel);

        await _dbContext.SaveChangesAsync();

        return imageDataModel.Id;
    }

    public async Task<ImageMetaDomainModel?> GetImageMetadataAsync(Guid imageId)
    {
        var metadata = await _dbContext.Images
            .Where(i => i.Id == imageId)
            .Select(i => new ImageMetaDomainModel
            {
                Id = i.Id,
                ContentType = i.ContentType
            })
            .FirstOrDefaultAsync();

        return metadata;
    }
}