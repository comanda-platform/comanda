namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Comanda.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

public static class ExpenseEndpoints
{
    public static void MapExpenseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/expenses")
            .WithTags("Expenses")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get expenses with optional filters")
            .WithDescription("Retrieves expenses. Use query parameters to filter: active, type, employeeId, locationId, activeOnDate");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get expense by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new expense");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchExpenseAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update expense fields")
            .WithDescription("Update expense description, amount, frequency, payment schedule, daily rate calculation, or end date");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete an expense");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] ExpenseQueryParameters query,
        ExpenseUseCase UseCase)
    {
        IEnumerable<Domain.Entities.Expense> expenses;

        // Apply filters based on query parameters
        if (query.Active.HasValue && query.Active.Value)
        {
            expenses = await UseCase.GetActiveAsync();
        }
        else if (query.Type.HasValue)
        {
            expenses = await UseCase.GetByTypeAsync(query.Type.Value);
        }
        else if (query.EmployeeId.HasValue)
        {
            expenses = await UseCase.GetByEmployeeAsync(query.EmployeeId.Value);
        }
        else if (query.LocationId.HasValue)
        {
            expenses = await UseCase.GetByLocationAsync(query.LocationId.Value);
        }
        else if (query.ActiveOnDate.HasValue)
        {
            expenses = await UseCase.GetActiveOnDateAsync(query.ActiveOnDate.Value);
        }
        else
        {
            expenses = await UseCase.GetAllAsync();
        }

        return Results.Ok(expenses.Select(ExpenseResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        ExpenseUseCase UseCase)
    {
        var expense = await UseCase.GetByPublicIdAsync(publicId);

        return Results.Ok(ExpenseResponseMapper.ToResponse(expense));
    }

    private static async Task<IResult> CreateAsync(
        CreateExpenseRequest request,
        ExpenseUseCase UseCase)
    {
        var expense = await UseCase.CreateAsync(
            request.Description,
            request.Type,
            request.Amount,
            request.Frequency,
            request.EffectiveFrom,
            request.DayOfMonth,
            request.DayOfWeek,
            request.SpecificPayableDate,
            request.DaysWorkedPerWeek,
            request.CalculateDailyRate,
            request.LocationPublicId,
            request.EmployeePublicId);

        return Results.Created(
            $"/api/expenses/{expense.PublicId}",
            ExpenseResponseMapper.ToResponse(expense));
    }

    private static async Task<IResult> PatchExpenseAsync(
        string publicId,
        PatchExpenseRequest request,
        ExpenseUseCase UseCase)
    {
        // Update description if provided
        if (!string.IsNullOrEmpty(request.Description))
        {
            await UseCase.UpdateDescriptionAsync(publicId, request.Description);
        }

        // Update amount if provided
        if (request.Amount.HasValue)
        {
            await UseCase.UpdateAmountAsync(publicId, request.Amount.Value);
        }

        // Update frequency if provided
        if (request.Frequency.HasValue)
        {
            await UseCase.UpdateFrequencyAsync(publicId, request.Frequency.Value);
        }

        // Update payment schedule if any schedule fields are provided
        if (request.DayOfMonth.HasValue || request.DayOfWeek.HasValue || request.SpecificPayableDate.HasValue)
        {
            await UseCase.SetPaymentScheduleAsync(
                publicId,
                request.DayOfMonth,
                request.DayOfWeek,
                request.SpecificPayableDate);
        }

        // Update daily rate calculation if provided
        if (request.CalculateDailyRate.HasValue)
        {
            await UseCase.SetDailyRateCalculationAsync(
                publicId,
                request.DaysWorkedPerWeek,
                request.CalculateDailyRate.Value);
        }

        // End expense if effectiveTo is provided
        if (request.EffectiveTo.HasValue)
        {
            await UseCase.EndAsync(publicId, request.EffectiveTo);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        ExpenseUseCase UseCase)
    {
        await UseCase.DeleteAsync(publicId);

        return Results.NoContent();
    }
}







