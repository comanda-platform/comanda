namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

public record CreateExpenseRequest(
    string Description,
    ExpenseType Type,
    decimal Amount,
    ExpenseFrequency Frequency,
    string LocationPublicId,
    string EmployeePublicId,
    DateTime? EffectiveFrom = null,
    int? DayOfMonth = null,
    DayOfWeek? DayOfWeek = null,
    DateTime? SpecificPayableDate = null,
    int? DaysWorkedPerWeek = null,
    bool CalculateDailyRate = false);

public record UpdateExpenseRequest(
    string? Description = null,
    decimal? Amount = null,
    ExpenseFrequency? Frequency = null);

public record SetPaymentScheduleRequest(
    int? DayOfMonth = null,
    DayOfWeek? DayOfWeek = null,
    DateTime? SpecificPayableDate = null);

public record SetDailyRateCalculationRequest(
    int? DaysWorkedPerWeek = null,
    bool CalculateDailyRate = false);

public record ExpenseResponse(
    string PublicId,
    string Description,
    ExpenseType Type,
    decimal Amount,
    ExpenseFrequency Frequency,
    DateTime? EffectiveFrom,
    DateTime? EffectiveTo,
    int? DayOfMonth,
    DayOfWeek? DayOfWeek,
    DateTime? SpecificPayableDate,
    int? DaysWorkedPerWeek,
    bool CalculateDailyRate,
    string LocationPublicId,
    string EmployeePublicId,
    DateTime CreatedAt);







