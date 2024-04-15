using GamesGlobal.Application.Validators.Interfaces;
using GamesGlobal.Enum;

namespace GamesGlobal.Application.Validators;

public class AttributeValidatorFactory : IAttributeValidatorFactory
{
    private readonly IEnumerable<IAttributeValidator> _attributeValidators;

    public AttributeValidatorFactory(IEnumerable<IAttributeValidator> attributeValidators)
    {
        _attributeValidators = attributeValidators;
    }

    public IAttributeValidator GetValidator(ProductSpecificationAttributeType attributeType)
    {
        return attributeType switch
        {
            ProductSpecificationAttributeType.Image =>
                _attributeValidators.First(a => a.GetType() == typeof(ImageAttributeValidator)),

            _ => throw new NotSupportedException($"No validator for attribute type: {attributeType}")
        };
    }
}