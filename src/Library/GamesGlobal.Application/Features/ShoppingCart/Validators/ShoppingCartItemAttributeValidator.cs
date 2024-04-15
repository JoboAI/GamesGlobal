using FluentValidation;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;

namespace GamesGlobal.Application.Features.ShoppingCart.Validators;

public class ShoppingCartItemAttributeValidator : AbstractValidator<IShoppingCartItemAttributeDomainModel>
{
    public ShoppingCartItemAttributeValidator()
    {
        RuleFor(attribute => attribute.ProductSpecificationAttributeId)
            .NotEmpty().WithMessage("Product Specification Attribute ID must be provided.");
    }
}