using GamesGlobal.Core.Interfaces.Repositories;
using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.DataAccess.Seed.Extensions;
using GamesGlobal.Infrastructure.Mappings;
using GamesGlobal.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IImageRepository, ImageRepository>()
            .AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

        services.AddDbContext<GamesGlobalDbContext>(opt =>
                opt.UseInMemoryDatabase("GamesGlobal"))
            .RegisterDatabaseSeeders();

        services.AddAutoMapper(typeof(ProductProfile).Assembly);
        return services;
    }
}