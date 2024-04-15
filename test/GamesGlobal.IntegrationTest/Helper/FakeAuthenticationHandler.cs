using System.Security.Claims;
using System.Text.Encodings.Web;
using GamesGlobal.IntegrationTest.TestData.Profiles;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GamesGlobal.IntegrationTest.Helper;

public class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public FakeAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Construct a fake identity and claims principal
        var identity = new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.NameIdentifier,
                ShoppingCartDatabaseSeed.AliceUserIdentifier.ToString()),
            new(ClaimTypes.Name, "Test User"),
            new("scp", "shopping-cart.checkout shopping-cart.manage products.read images.write") // add scopes
        }, "Test");

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}