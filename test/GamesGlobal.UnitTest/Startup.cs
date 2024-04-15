using FluentValidation;
using GamesGlobal.Application.Features.ShoppingCart.Commands;
using GamesGlobal.Core.Interfaces.Repositories;
using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.Mappings;
using GamesGlobal.Infrastructure.Repositories;
using GamesGlobal.UnitTest.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.UnitTest;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var dbOptions = InMemoryDatabaseHelper.CreateNewContextOptions();
        services.AddSingleton(new GamesGlobalDbContext(new TestUserContext(), dbOptions));
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        // Add FluentValidation Validators
        services.AddValidatorsFromAssemblyContaining<AddItemToCartCommand>();
        services.AddAutoMapper(typeof(ProductProfile).Assembly);
    }
}