namespace Comanda.Api.Filters;

public sealed class RequirePublicIdFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var publicId = context.GetArgument<string>(0);

        if (string.IsNullOrWhiteSpace(publicId))
            return Results.BadRequest("PublicId is required");

        return await next(context);
    }
}







