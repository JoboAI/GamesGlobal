using GamesGlobal.Enum;

namespace GamesGlobal.Application.Validators.Interfaces;

public interface IAttributeValidatorFactory
{
    IAttributeValidator GetValidator(ProductSpecificationAttributeType attributeType);
}