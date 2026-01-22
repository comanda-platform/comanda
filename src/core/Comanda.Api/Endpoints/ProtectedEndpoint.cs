namespace Comanda.Api.Endpoints;

using System.Security.Claims;

public static class ProtectedEndpoints
{
    public static void MapProtectedEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/protected", (ClaimsPrincipal user) =>
        {
            return Results.Ok(new
            {
                Message = "This is a protected endpoint",
                UserId = user.FindFirstValue(ClaimTypes.NameIdentifier),
                UserName = user.FindFirstValue(ClaimTypes.Name)
            });
        })
        .RequireAuthorization()
        .WithName("ProtectedEndpoint")
        .WithTags("Protected");
    }
}







