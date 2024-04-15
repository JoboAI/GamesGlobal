using AutoMapper;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;
using GamesGlobal.Enum;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.ShoppingCartItemAttribute;

namespace GamesGlobal.Infrastructure.Mappings;

public class ShoppingCartItemAttributeProfile : Profile
{
    public ShoppingCartItemAttributeProfile()
    {
        CreateMap<ShoppingCartItemAttributeDataModel, IShoppingCartItemAttributeDomainModel>()
            .ConvertUsing((src, context) => MapToDomainModel(src));

        CreateMap<IShoppingCartItemAttributeDomainModel, ShoppingCartItemAttributeDataModel>()
            .ConvertUsing((src, context) => MapToDataModel(src));
    }

    private static IShoppingCartItemAttributeDomainModel MapToDomainModel(
        ShoppingCartItemAttributeDataModel src)
    {
        switch (src.Value.AttributeType)
        {
            case ProductSpecificationAttributeType.Image:
                return MapToImageDomainModel(src);
            default:
                throw new InvalidOperationException("Unsupported attribute type");
        }
    }

    private static ShoppingCartItemAttributeDataModel MapToDataModel(
        IShoppingCartItemAttributeDomainModel src)
    {
        if (src is ImageShoppingCartItemAttributeDomainModel imageDomainModel)
            return MapToImageDataModel(imageDomainModel);

        throw new InvalidOperationException("Unsupported domain model type");
    }

    private static ImageShoppingCartItemAttributeDomainModel MapToImageDomainModel(
        ShoppingCartItemAttributeDataModel src)
    {
        return new ImageShoppingCartItemAttributeDomainModel
        {
            Id = src.Id,
            ProductSpecificationAttributeId = src.ProductSpecificationAttributeId,
            AttributeType = src.Value.AttributeType,
            ImageId = (src.Value as ImageShoppingCartItemAttributeValueDataModel)!.ImageId
        };
    }

    private static ShoppingCartItemAttributeDataModel MapToImageDataModel(
        ImageShoppingCartItemAttributeDomainModel imageDomainModel)
    {
        var configModel = new ImageShoppingCartItemAttributeValueDataModel
        {
            ImageId = imageDomainModel.ImageId,
            AttributeType = imageDomainModel.AttributeType
        };

        var dataModel = new ShoppingCartItemAttributeDataModel
        {
            Id = imageDomainModel.Id,
            ProductSpecificationAttributeId = imageDomainModel.ProductSpecificationAttributeId,
            Value = configModel
        };

        return dataModel;
    }
}