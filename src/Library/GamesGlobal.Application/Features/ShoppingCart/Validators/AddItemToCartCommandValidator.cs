using FluentValidation;
using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Application.Features.ShoppingCart.Validators.Attributes;

namespace GamesGlobal.Application.Features.ShoppingCart.Validators;

public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
{
    public AddItemToCartCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID must be provided.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleForEach(command => command.Attributes).SetInheritanceValidator(v =>
        {
            v.Add(new ShoppingCartItemAttributeValidator());
            v.Add(new ImageShoppingCartItemAttributeValidator());
        });
    }
}