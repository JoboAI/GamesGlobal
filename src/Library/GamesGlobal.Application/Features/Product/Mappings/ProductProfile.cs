using AutoMapper;
using GamesGlobal.Application.Features.Product.Responses;
using GamesGlobal.Core.Entities;

namespace GamesGlobal.Application.Features.Product.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductDomainModel, ProductResponse>();
    }
}