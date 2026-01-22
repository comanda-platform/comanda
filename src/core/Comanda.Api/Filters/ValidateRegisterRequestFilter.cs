using Microsoft.AspNetCore.Http;
using Comanda.Api.Models;

namespace Comanda.Api.Filters;

public sealed class ValidateRegisterRequestFilter : IEndpointFilter
{
    public ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.GetArgument<RegisterRequest>(0);

        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.Username))
            errors[nameof(RegisterRequest.Username)] = ["Username is required"];

        if (string.IsNullOrWhiteSpace(request.Email))
            errors[nameof(RegisterRequest.Email)] = ["Email is required"];

        if (string.IsNullOrWhiteSpace(request.Password))
            errors[nameof(RegisterRequest.Password)] = ["Password is required"];

        if (errors.Count > 0)
        {
            return ValueTask.FromResult<object?>(
                Results.ValidationProblem(errors, detail: "Validation failed"));
        }

        return next(context);
    }
}







