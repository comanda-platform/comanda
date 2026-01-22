namespace Comanda.Api.Filters;

public sealed class RequireNonEmptyStringFilter : IEndpointFilter
{
    private readonly int _argumentIndex;
    private readonly string _parameterName;

    public RequireNonEmptyStringFilter(int argumentIndex, string parameterName)
    {
        _argumentIndex = argumentIndex;
        _parameterName = parameterName;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var value = context.GetArgument<string>(_argumentIndex);

        if (string.IsNullOrWhiteSpace(value))
            return Results.BadRequest($"{_parameterName} is required");

        return await next(context);
    }
}







