namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employees")
            .WithTags("Employees")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get employees with optional filters")
            .WithDescription("Retrieves employees. Use query parameters to filter: active, userName, email");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get employee by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new employee");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchEmployeeAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update employee fields or status")
            .WithDescription("Update employee details, activate/deactivate, or manage API keys");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete an employee");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] EmployeeQueryParameters query,
        EmployeeUseCase UseCase)
    {
        IEnumerable<Domain.Entities.Employee> employees;

        // Apply filters based on query parameters
        if (query.Active.HasValue && query.Active.Value)
        {
            employees = await UseCase.GetActiveEmployeesAsync();
        }
        else if (!string.IsNullOrEmpty(query.UserName))
        {
            var employee = await UseCase.GetEmployeeByUserNameAsync(query.UserName);
            employees = new[] { employee };
        }
        else if (!string.IsNullOrEmpty(query.Email))
        {
            var employee = await UseCase.GetEmployeeByEmailAsync(query.Email);
            employees = new[] { employee };
        }
        else
        {
            employees = await UseCase.GetAllEmployeesAsync();
        }

        return Results.Ok(employees.Select(EmployeeResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        EmployeeUseCase UseCase)
    {
        var employee = await UseCase.GetEmployeeByPublicIdAsync(publicId);

        return Results.Ok(EmployeeResponseMapper.ToResponse(employee));
    }

    private static async Task<IResult> CreateAsync(
        CreateEmployeeRequest request,
        EmployeeUseCase UseCase)
    {
        var employee = await UseCase.CreateEmployeeAsync(
            request.UserName,
            request.Email,
            request.PhoneNumber);

        return Results.Created(
            $"/api/employees/{employee.PublicId}",
            EmployeeResponseMapper.ToResponse(employee));
    }

    private static async Task<IResult> PatchEmployeeAsync(
        string publicId,
        PatchEmployeeRequest request,
        EmployeeUseCase UseCase)
    {
        // Update basic fields if any are provided
        if (!string.IsNullOrEmpty(request.UserName) ||
            !string.IsNullOrEmpty(request.Email) ||
            !string.IsNullOrEmpty(request.PhoneNumber))
        {
            await UseCase.UpdateEmployeeAsync(
                publicId,
                request.UserName,
                request.Email,
                request.PhoneNumber);
        }

        // Update active status if provided
        if (request.IsActive.HasValue)
        {
            if (request.IsActive.Value)
            {
                await UseCase.ActivateEmployeeAsync(publicId);
            }
            else
            {
                await UseCase.DeactivateEmployeeAsync(publicId);
            }
        }

        // Generate API key if requested
        if (request.GenerateApiKey.HasValue && request.GenerateApiKey.Value)
        {
            var apiKey = await UseCase.GenerateApiKeyAsync(publicId);
            return Results.Ok(new { apiKey });
        }

        // Revoke API key if requested
        if (request.RevokeApiKey.HasValue && request.RevokeApiKey.Value)
        {
            await UseCase.RevokeApiKeyAsync(publicId);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        EmployeeUseCase UseCase)
    {
        await UseCase.DeleteEmployeeAsync(publicId);

        return Results.NoContent();
    }
}






