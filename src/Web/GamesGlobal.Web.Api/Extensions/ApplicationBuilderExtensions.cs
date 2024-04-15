namespace GamesGlobal.Web.Api.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static void ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Games Global V1");
            c.OAuthClientId(app.Configuration["AzureAdB2C:ClientId"]);
            c.OAuthUsePkce();
        });
    }
}