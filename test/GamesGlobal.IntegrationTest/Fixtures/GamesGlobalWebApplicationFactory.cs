using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.IntegrationTest.Helper;
using GamesGlobal.IntegrationTest.TestData;
using GamesGlobal.IntegrationTest.TestData.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.IntegrationTest.Fixtures;

public class GamesGlobalWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<GamesGlobalDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            services.AddDbContext<GamesGlobalDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });

            services.RegisterTestDataSeeders();
            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<GamesGlobalDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.SeedTestDataAsync(scope.ServiceProvider).GetAwaiter().GetResult();
            }


            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>(
                    "Test", options => { });
        });

        builder.UseEnvironment("Testing");
    }
}