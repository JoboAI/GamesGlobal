using AutoMapper;
using GamesGlobal.Application;
using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Infrastructure;
using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.DataAccess.Seed;
using GamesGlobal.Web.Api.Extensions;
using GamesGlobal.Web.Api.Middlewares;
using GamesGlobal.Web.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddControllersWithValidation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.RegisterSwagger(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseGamesGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    var mapper = app.Services.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
    app.ConfigureSwagger();
}


using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<GamesGlobalDbContext>();
        if (app.Environment.IsDevelopment()) await context.SeedAsync(scopedProvider);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Make sure to call this before `UseAuthorization`
app.UseAuthorization();

app.MapControllers();

app.Run();

namespace GamesGlobal.Web.Api
{
    /// <summary>
    ///     Added for test host
    /// </summary>
    public abstract class Program
    {
    }
}