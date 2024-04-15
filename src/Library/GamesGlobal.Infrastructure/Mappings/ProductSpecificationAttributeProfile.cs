using AutoMapper;
using GamesGlobal.Core.Entities.ProductSpecificationAttributes;
using GamesGlobal.Enum;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;

namespace GamesGlobal.Infrastructure.Mappings;

public class ProductSpecificationAttributeProfile : Profile
{
    public ProductSpecificationAttributeProfile()
    {
        CreateMap<ProductSpecificationAttributeDataModel, IProductSpecificationAttributeDomainModel>()
            .ConvertUsing((src, context) => MapToDomainModel(src));

        CreateMap<IProductSpecificationAttributeDomainModel, ProductSpecificationAttributeDataModel>()
            .ConvertUsing((src, context) => MapToDataModel(src));
    }

    private static IProductSpecificationAttributeDomainModel MapToDomainModel(
        ProductSpecificationAttributeDataModel src)
    {
        switch (src.Configuration.AttributeType)
        {
            case ProductSpecificationAttributeType.Image:
                return MapToImageDomainModel(src);
            default:
                throw new InvalidOperationException("Unsupported attribute type");
        }
    }

    private static ProductSpecificationAttributeDataModel MapToDataModel(
        IProductSpecificationAttributeDomainModel src)
    {
        if (src is ImageProductSpecificationAttributeDomainModel imageDomainModel)
            return MapToImageDataModel(imageDomainModel);

        throw new InvalidOperationException("Unsupported domain model type");
    }

    private static ImageProductSpecificationAttributeDomainModel MapToImageDomainModel(
        ProductSpecificationAttributeDataModel src)
    {
        return new ImageProductSpecificationAttributeDomainModel
        {
            Id = src.Id,
            Label = src.Label,
            IsRequired = src.IsRequired,
            AttributeType = src.Configuration.AttributeType,
            ContentType = (src.Configuration as ImageProductSpecificationAttributeConfigurationDataModel)!.ContentType
        };
    }

    private static ProductSpecificationAttributeDataModel MapToImageDataModel(
        ImageProductSpecificationAttributeDomainModel imageDomainModel)
    {
        var configModel = new ImageProductSpecificationAttributeConfigurationDataModel
        {
            ContentType = imageDomainModel.ContentType
        };

        var dataModel = new ProductSpecificationAttributeDataModel
        {
            Id = imageDomainModel.Id,
            Label = imageDomainModel.Label,
            IsRequired = imageDomainModel.IsRequired,
            Configuration = configModel
        };

        return dataModel;
    }
}