using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;

namespace Comanda.Client.Admin.Infrastructure.Auth;

public class BlazorAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BlazorAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Return authenticated identity to satisfy server-side authorization
        // Blazor's CustomAuthStateProvider handles the actual JWT authentication
        var claims = new[] { new Claim(ClaimTypes.Name, "BlazorUser") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
