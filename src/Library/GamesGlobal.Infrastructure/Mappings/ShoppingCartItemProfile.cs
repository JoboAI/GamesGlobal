using AutoMapper;
using GamesGlobal.Core.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities;

namespace GamesGlobal.Infrastructure.Mappings;

public class ShoppingCartItemProfile : Profile
{
    public ShoppingCartItemProfile()
    {
        // The item's unit price is resolved from a nested Product entity.
        // ReverseMap addresses relational context loss when converting back to the DataModel.
        CreateMap<ShoppingCartItemDataModel, ShoppingCartItemDomainModel>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes));

        // Explicit Ignore for reverse mapping is critical to avoid unintentional
        // entity state changes as DbContext tracks changes based on navigation property references.
        CreateMap<ShoppingCartItemDomainModel, ShoppingCartItemDataModel>()
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes))
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.ShoppingCart, opt => opt.Ignore())
            .ForMember(dest => dest.ShoppingCartId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore());
    }
}