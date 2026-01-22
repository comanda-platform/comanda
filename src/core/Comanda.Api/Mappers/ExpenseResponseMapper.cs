using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class ExpenseResponseMapper
{
    public static ExpenseResponse ToResponse(Domain.Entities.Expense expense) 
        => new(
            expense.PublicId,
            expense.Description,
            expense.Type,
            expense.Amount,
            expense.Frequency,
            expense.EffectiveFrom,
            expense.EffectiveTo,
            expense.DayOfMonth,
            expense.DayOfWeek,
            expense.SpecificPayableDate,
            expense.DaysWorkedPerWeek,
            expense.CalculateDailyRate,
            expense.LocationPublicId,
            expense.EmployeePublicId,
            expense.CreatedAt);
}






