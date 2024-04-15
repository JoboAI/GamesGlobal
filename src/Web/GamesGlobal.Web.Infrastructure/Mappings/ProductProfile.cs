using AutoMapper;
using GamesGlobal.Application.Features.Product.Responses;
using GamesGlobal.Web.Infrastructure.Dtos;

namespace GamesGlobal.Web.Infrastructure.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductDto, ProductResponse>();
    }
}