using GamesGlobal.Application.Validators.Interfaces;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Entities;

namespace GamesGlobal.Application.Validators;

public class ShoppingCartItemValidator : IShoppingCartItemValidator
{
    private readonly IAttributeValidatorFactory _validatorFactory;

    public ShoppingCartItemValidator(IAttributeValidatorFactory validatorFactory)
    {
        _validatorFactory = validatorFactory;
    }

    public async Task<IResult> ValidateCartItem(ShoppingCartItemDomainModel shoppingCartItem,
        ProductDomainModel product)
    {
        var errors = new List<string>();

        // Loop through all the required specification attributes of the product
        foreach (var specificationAttribute in product.SpecificationAttributes.Where(sa => sa.IsRequired))
        {
            var attribute = shoppingCartItem.Attributes
                .FirstOrDefault(a => a.ProductSpecificationAttributeId == specificationAttribute.Id);

            if (attribute == null)
            {
                errors.Add($"Missing required attribute: {specificationAttribute.Label}");
                continue;
            }

            var validator = _validatorFactory.GetValidator(attribute.AttributeType);
            var validationErrors = await validator.ValidateAsync(attribute, specificationAttribute);

            errors.AddRange(validationErrors);
        }

        // If any errors were accumulated, return a failure result with all error messages
        if (errors.Any()) return await Result.FailAsync(errors);

        // If no errors, return success
        return await Result.SuccessAsync("Validation of the cart item succeeded");
    }
}