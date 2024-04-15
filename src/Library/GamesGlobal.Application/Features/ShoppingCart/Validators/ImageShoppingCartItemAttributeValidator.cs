using FluentValidation;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;

namespace GamesGlobal.Application.Features.ShoppingCart.Validators.Attributes;

public class ImageShoppingCartItemAttributeValidator : AbstractValidator<ImageShoppingCartItemAttributeDomainModel>
{
    public ImageShoppingCartItemAttributeValidator()
    {
        Include(new ShoppingCartItemAttributeValidator());

        RuleFor(attribute => attribute.ImageId)
            .NotEmpty().WithMessage("Image ID must be provided.");
    }
}