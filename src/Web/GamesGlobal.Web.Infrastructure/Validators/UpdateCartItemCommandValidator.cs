using FluentValidation;
using GamesGlobal.Application.Features.ShoppingCart.Commands;

namespace GamesGlobal.Web.Infrastructure.Validators;

public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
{
    public UpdateCartItemCommandValidator()
    {
        RuleFor(x => x.ItemId)
            .NotEmpty().WithMessage("Item ID must be provided.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}