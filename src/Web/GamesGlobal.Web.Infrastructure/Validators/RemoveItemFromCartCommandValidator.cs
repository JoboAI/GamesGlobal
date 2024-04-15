using FluentValidation;
using GamesGlobal.Web.Infrastructure.Dtos;

namespace GamesGlobal.Web.Infrastructure.Validators;

public class ShoppingCartItemUpdateDtoValidator : AbstractValidator<ShoppingCartItemUpdateDto>
{
    public ShoppingCartItemUpdateDtoValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}