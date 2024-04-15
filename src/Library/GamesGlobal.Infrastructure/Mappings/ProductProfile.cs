using AutoMapper;
using GamesGlobal.Core.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities;

namespace GamesGlobal.Infrastructure.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductDataModel, ProductDomainModel>()
            .ReverseMap()
            .ForMember(dest => dest.ShoppingCartItems, opt => opt.Ignore());
    }
}