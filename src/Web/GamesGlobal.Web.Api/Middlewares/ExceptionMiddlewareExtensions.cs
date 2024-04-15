using System.Net;
using System.Text.Json;
using FluentValidation;
using GamesGlobal.Common.Wrappers;
using Microsoft.AspNetCore.Diagnostics;

namespace GamesGlobal.Web.Api.Middlewares;

/// <summary>
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    public static void UseGamesGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(err =>
        {
            err.Run(async ctx =>
            {
                var exception = ctx.Features.Get<IExceptionHandlerFeature>();
                ctx.Response.ContentType = "application/json";

                if (exception != null)
                {
                    string resultMessage;
                    List<string> errors = null;

                    switch (ctx.Response.StatusCode)
                    {
                        case (int)HttpStatusCode.BadRequest:
                            var errorResponse =
                                JsonSerializer.Deserialize<Dictionary<string, object>>(
                                    await new StreamReader(ctx.Response.Body).ReadToEndAsync());
                            if (errorResponse != null && errorResponse.TryGetValue("errors", out var value))
                            {
                                var errorDetails = value as Dictionary<string, object>;
                                errors = new List<string>();
                                foreach (var error in errorDetails)
                                    errors.AddRange(((JsonElement)error.Value).EnumerateArray()
                                        .Select(e => e.GetString())!);
                            }

                            resultMessage = "Validation failed";
                            break;

                        default:
                            resultMessage = "An unexpected error occurred. Please try again later.";
                            break;
                    }

                    switch (exception.Error)
                    {
                        case ValidationException validationException:
                            ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            errors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                            break;
                    }

                    var response = errors != null
                        ? await Result.FailAsync(errors)
                        : await Result.FailAsync(resultMessage);
                    await ctx.Response.WriteAsync(JsonSerializer.Serialize(response, SerializerOptions));
                }
            });
        });
    }
}