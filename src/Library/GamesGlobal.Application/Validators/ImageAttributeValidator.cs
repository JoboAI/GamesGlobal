using GamesGlobal.Application.Validators.Interfaces;
using GamesGlobal.Core.Entities.ProductSpecificationAttributes;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;
using GamesGlobal.Core.Interfaces.Repositories;

namespace GamesGlobal.Application.Validators;

public class ImageAttributeValidator : IAttributeValidator
{
    private readonly IImageRepository _imageRepository;

    public ImageAttributeValidator(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task<IEnumerable<string>> ValidateAsync(
        IShoppingCartItemAttributeDomainModel attribute,
        IProductSpecificationAttributeDomainModel productAttribute)
    {
        var errors = new List<string>();

        if (!(attribute is ImageShoppingCartItemAttributeDomainModel typedAttribute) ||
            !(productAttribute is ImageProductSpecificationAttributeDomainModel typedProductAttribute))
        {
            errors.Add("Invalid image attribute type.");
            return errors;
        }

        var imageMeta = await _imageRepository.GetImageMetadataAsync(typedAttribute.ImageId);
        if (imageMeta == null)
        {
            errors.Add($"Image with ID: {typedAttribute.ImageId} not found.");
            return errors;
        }

        if (!typedProductAttribute.ContentType.Contains(imageMeta.ContentType))
            errors.Add($"Invalid content type for image attribute: {productAttribute.Label}");

        return errors;
    }
}