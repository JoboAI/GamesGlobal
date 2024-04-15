using AutoMapper;
using GamesGlobal.Core.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities;

namespace GamesGlobal.Infrastructure.Mappings;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        // ShoppingCartDataModel and ShoppingCartDomainModel have equivalent
        // collections of items. Mapping configured for bi-directional conversion.
        CreateMap<ShoppingCartDataModel, ShoppingCartDomainModel>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<ShoppingCartDomainModel, ShoppingCartDataModel>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.UserIdentifier, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore());
    }
}