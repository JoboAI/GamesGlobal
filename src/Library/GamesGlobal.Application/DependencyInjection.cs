using FluentValidation;
using GamesGlobal.Application.Features.Product.Mappings;
using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Application.Features.ShoppingCart.Validators;
using GamesGlobal.Application.Validators;
using GamesGlobal.Application.Validators.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddItemToCartCommandValidator>();
        services.AddAutoMapper(typeof(ProductProfile).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.RegisterValidators();
        return services.AddMediatR(typeof(AddItemToCartCommand).Assembly);
    }

    private static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddScoped<IAttributeValidatorFactory, AttributeValidatorFactory>();
        services.AddScoped<IShoppingCartItemValidator, ShoppingCartItemValidator>();
        services.AddScoped<IAttributeValidator, ImageAttributeValidator>();

        return services;
    }
}