using Comanda.Application.UseCases;
using System.Security.Claims;

namespace Comanda.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/user/me", GetCurrentUserAsync)
            .RequireAuthorization()
            .WithName("GetCurrentUser")
            .WithTags("User");
    }

    private static async Task<IResult> GetCurrentUserAsync(
        ClaimsPrincipal user,
        AuthenticationUseCase authUseCase)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdClaim is null || !int.TryParse(userIdClaim, out var userId))
            return Results.Unauthorized();

        var employee = await authUseCase.GetCurrentEmployeeAsync(userId);

        if (employee == null)
            return Results.NotFound();

        return Results.Ok(new
        {
            employee.PublicId,
            employee.UserName,
            employee.Email,
            HasApiKey = !string.IsNullOrEmpty(employee.ApiKey),
            employee.ApiKeyCreatedAt
        });
    }
}







