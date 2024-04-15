using System.Security.Claims;
using GamesGlobal.Core.Interfaces.Common;
using Microsoft.AspNetCore.Http;

namespace GamesGlobal.Web.Infrastructure.Common;

/// <summary>
///     Concrete implementation of IUserContext for accessing the current user's context in a web environment.
/// </summary>
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserContext" /> class.
    /// </summary>
    /// <param name="httpContextAccessor">The service used to access the <see cref="HttpContext" />.</param>
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    ///     Retrieves the current user's unique identifier as a string.
    /// </summary>
    /// <returns>The current user's ID as a string.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no user is logged in or if the user ID is invalid.</exception>
    public string GetUserId()
    {
        // Get the current user's claims principal
        var currentUser = _httpContextAccessor.HttpContext?.User;
        if (currentUser == null) return Guid.Empty.ToString();

        // Retrieve the user ID from the NameIdentifier claim type
        var userIdString = currentUser.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            throw new InvalidOperationException("The current user ID is invalid.");

        return userIdString;
    }
}