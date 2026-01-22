using Microsoft.AspNetCore.Identity;
using Comanda.Api.Filters;
using Comanda.Api.Models;
using Comanda.Application.UseCases;

namespace Comanda.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/register", RegisterAsync)
           .AddEndpointFilter<ValidateRegisterRequestFilter>()
           .WithTags("Auth");

        app.MapPost("/api/auth/login", LoginAsync)
            .WithName("Login")
            .WithTags("Auth");
    }

    private static async Task<IResult> RegisterAsync(
        RegisterRequest request,
        AuthenticationUseCase authUseCase)
    {
        var result = await authUseCase.RegisterAsync(
            request.Username.Trim(),
            request.Email.Trim(),
            request.Password);

        if (!result.Success)
        {
            var errors = (result.Errors ?? [])
                .GroupBy(MapRegistrationErrorKey)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToArray());

            return Results.ValidationProblem(errors, detail: "Registration failed");
        }

        return Results.Ok(new AuthResponse(
            Token: result.Token!,
            Email: result.Employee!.Email,
            Username: result.Employee.UserName
        ));
    }

    private static async Task<IResult> LoginAsync(
        LoginRequest request,
        AuthenticationUseCase authUseCase)
    {
        var result = await authUseCase.LoginAsync(
            request.Email,
            request.Password);

        if (!result.Success)
            return Results.Unauthorized();

        return Results.Ok(new AuthResponse(
            Token: result.Token!,
            Email: result.Employee!.Email,
            Username: result.Employee.UserName,
            ApiKey: result.Employee.ApiKey
        ));
    }

    private static string MapRegistrationErrorKey(string error)
    {
        if (error.Contains("UserName", StringComparison.OrdinalIgnoreCase) ||
            error.Contains("Username", StringComparison.OrdinalIgnoreCase))
            return nameof(RegisterRequest.Username);

        if (error.Contains("Email", StringComparison.OrdinalIgnoreCase))
            return nameof(RegisterRequest.Email);

        if (error.Contains("Password", StringComparison.OrdinalIgnoreCase))
            return nameof(RegisterRequest.Password);

        return "General";
    }
}







