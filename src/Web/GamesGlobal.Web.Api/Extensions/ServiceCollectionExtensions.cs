using FluentValidation.AspNetCore;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Web.Infrastructure.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace GamesGlobal.Web.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddControllersWithValidation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateCartItemCommandValidator>())
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var result = new Result
                    {
                        Succeeded = false,
                        Messages = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });
    }

    private static string GetScopeUri(IConfiguration configuration, string scope)
    {
        // Here we build the scope URL from the configuration and the provided scope name
        return $"https://{configuration["AzureAdB2C:Domain"]}/{configuration["AzureAdB2C:ClientId"]}/{scope}";
    }

    internal static void RegisterSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            // include all project's xml comments
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                if (!assembly.IsDynamic)
                {
                    var xmlFile = $"{assembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(baseDirectory, xmlFile);
                    if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
                }

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Games Global API"
            });

            var authBaseUrl =
                $"{configuration["AzureAdB2C:Instance"]}/{configuration["AzureAdB2C:Domain"]}/{configuration["AzureAdB2C:SignUpSignInPolicyId"]}/oauth2/v2.0";

            // Configure OAuth2 for Swagger
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{authBaseUrl}/authorize"),
                        TokenUrl = new Uri($"{authBaseUrl}/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "Perform user sign-in operations" },
                            { GetScopeUri(configuration, "images.write"), "Write access to images" },
                            { GetScopeUri(configuration, "products.read"), "Read access to products" },
                            { GetScopeUri(configuration, "shopping-cart.manage"), "Manage shopping cart" },
                            { GetScopeUri(configuration, "shopping-cart.checkout"), "Perform checkout operations" }
                        }
                    }
                }
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] { "openid" }
                }
            });
        });
    }
}