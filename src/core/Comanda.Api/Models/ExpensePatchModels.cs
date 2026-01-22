namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

/// <summary>
/// Request model for patching an expense
/// </summary>
public record PatchExpenseRequest
{
    public string? Description { get; init; }
    public decimal? Amount { get; init; }
    public ExpenseFrequency? Frequency { get; init; }
    public int? DayOfMonth { get; init; }
    public DayOfWeek? DayOfWeek { get; init; }
    public DateTime? SpecificPayableDate { get; init; }
    public int? DaysWorkedPerWeek { get; init; }
    public bool? CalculateDailyRate { get; init; }
    public DateTime? EffectiveTo { get; init; }
}

/// <summary>
/// Query parameters for filtering expenses
/// </summary>
public record ExpenseQueryParameters
{
    public bool? Active { get; init; }
    public ExpenseType? Type { get; init; }
    public int? EmployeeId { get; init; }
    public int? LocationId { get; init; }
    public DateTime? ActiveOnDate { get; init; }
}







