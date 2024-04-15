using FluentValidation;
using GamesGlobal.Application.Features.ShoppingCart.Commands;

namespace GamesGlobal.Application.Features.ShoppingCart.Validators;

public class RemoveItemFromCartCommandValidator : AbstractValidator<RemoveItemFromCartCommand>
{
    public RemoveItemFromCartCommandValidator()
    {
        RuleFor(x => x.ItemId)
            .NotEmpty().WithMessage("Item ID must be provided.");
    }
}