using AutoMapper;
using GamesGlobal.Application.Features.ShoppingCart.Responses;
using GamesGlobal.Web.Infrastructure.Dtos;

namespace GamesGlobal.Web.Infrastructure.Mappings;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<ShoppingCartDto, GetShoppingCartQueryResponse>();
    }
}