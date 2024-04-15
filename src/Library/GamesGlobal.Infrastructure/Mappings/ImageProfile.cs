using AutoMapper;
using GamesGlobal.Core.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities;

namespace GamesGlobal.Infrastructure.Mappings;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<ImageDataModel, ImageDomainModel>()
            .ReverseMap();
    }
}