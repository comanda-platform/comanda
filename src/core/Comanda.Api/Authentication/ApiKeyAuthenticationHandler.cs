using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Comanda.Application.UseCases;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Comanda.Api.Authentication;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "ApiKey";
    public string Scheme => DefaultScheme;
    public string ApiKeyHeaderName { get; set; } = "X-Api-Key";
}

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<ApiKeyAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    EmployeeUseCase employeeUseCase)
    : AuthenticationHandler<ApiKeyAuthenticationOptions>(options, logger, encoder)
{
    private readonly EmployeeUseCase _employeeUseCase = employeeUseCase;

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(Options.ApiKeyHeaderName, out var apiKeyHeaderValues))
        {
            return AuthenticateResult.NoResult();
        }

        var providedApiKey = apiKeyHeaderValues.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(providedApiKey))
        {
            return AuthenticateResult.NoResult();
        }

        var employee = await _employeeUseCase.GetEmployeeByApiKeyAsync(providedApiKey);

        if (employee == null)
        {
            return AuthenticateResult.Fail("Invalid API Key");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, employee.PublicId),
            new Claim(ClaimTypes.Name, employee.UserName),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim("PublicId", employee.PublicId)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}







