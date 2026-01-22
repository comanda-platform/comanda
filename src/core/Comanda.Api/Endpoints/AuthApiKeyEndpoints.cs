using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Comanda.Application.UseCases;
using System.Security.Claims;

namespace Comanda.Api.Endpoints;

public static class AuthApiKeyEndpoints
{
    public static void MapAuthApiKeyEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/api-keys", CreateAsync)
            .RequireAuthorization()
            .WithName("GenerateApiKey")
            .WithTags("Auth");
    }

    private static async Task<IResult> CreateAsync(
        ClaimsPrincipal user,
        AuthenticationUseCase authUseCase)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdClaim is null || !int.TryParse(userIdClaim, out var userId))
            return Results.Unauthorized();

        var apiKey = await authUseCase.GenerateApiKeyAsync(userId);
        return Results.Ok(new { ApiKey = apiKey });
    }
}







