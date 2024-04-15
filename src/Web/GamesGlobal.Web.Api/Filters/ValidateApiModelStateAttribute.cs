using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GamesGlobal.Web.Api.Filters;

/// <summary>
///     An action filter that validates the model state before executing the action.
///     If the model state is invalid, the filter short-circuits the request by returning a BadRequest result.
/// </summary>
public class ValidateApiModelStateAttribute : ActionFilterAttribute
{
    /// <summary>
    ///     Called before the action executes, interrupting execution if the model state is invalid.
    /// </summary>
    /// <param name="context">The context for the action filters with data about the current request and action.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Check if the model state is valid, if not, return a BadRequest with the model state errors.
        if (!context.ModelState.IsValid) context.Result = new BadRequestObjectResult(context.ModelState);
    }
}