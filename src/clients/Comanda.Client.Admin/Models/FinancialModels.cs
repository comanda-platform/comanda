using Comanda.Shared.Enums;

namespace Comanda.Client.Admin.Models;

// Ledger Entry models
public record LedgerEntryResponse(
    string PublicId,
    string ClientPublicId,
    DateTime OccurredAt,
    decimal Amount,
    LedgerEntryType Type,
    PaymentMethod? PaymentMethod,
    string? OrderLinePublicId);

public record CreateCreditRequest(
    string ClientPublicId,
    decimal Amount,
    string? OrderLinePublicId);

public record CreatePaymentRequest(
    string ClientPublicId,
    decimal Amount,
    PaymentMethod PaymentMethod);

public record CreateAdjustmentRequest(
    string ClientPublicId,
    decimal Amount);

public record CreateWriteOffRequest(
    string ClientPublicId,
    decimal Amount);

// Expense models
public record ExpenseResponse(
    string PublicId,
    string Description,
    ExpenseType Type,
    decimal Amount,
    ExpenseFrequency Frequency,
    DateTime EffectiveFrom,
    DateTime? EffectiveTo,
    DateTime CreatedAt,
    int? DayOfMonth,
    DayOfWeek? DayOfWeek,
    DateTime? SpecificPayableDate,
    int? DaysWorkedPerWeek,
    bool CalculateDailyRate,
    string? LocationPublicId,
    string? EmployeePublicId);

public record CreateExpenseRequest(
    string Description,
    ExpenseType Type,
    decimal Amount,
    ExpenseFrequency Frequency,
    DateTime? EffectiveFrom,
    int? DayOfMonth,
    DayOfWeek? DayOfWeek,
    DateTime? SpecificPayableDate,
    int? DaysWorkedPerWeek,
    bool CalculateDailyRate,
    string? LocationPublicId,
    string? EmployeePublicId);

public record UpdateExpenseRequest(
    string Description,
    decimal Amount,
    ExpenseFrequency Frequency);

public record EndExpenseRequest(
    DateTime? EffectiveTo);

public static class LedgerExtensions
{
    public static bool IsDebit(this LedgerEntryResponse entry) => entry.Amount < 0;
    public static bool IsCredit(this LedgerEntryResponse entry) => entry.Amount > 0;
    public static decimal AbsoluteAmount(this LedgerEntryResponse entry) => Math.Abs(entry.Amount);
}

public static class ExpenseExtensions
{
    public static bool IsActive(this ExpenseResponse expense) =>
        !expense.EffectiveTo.HasValue || expense.EffectiveTo.Value > DateTime.UtcNow;

    public static decimal GetDailyRate(this ExpenseResponse expense)
    {
        if (!expense.CalculateDailyRate || !expense.DaysWorkedPerWeek.HasValue || expense.DaysWorkedPerWeek.Value == 0)
            return expense.Amount;

        return expense.Amount / expense.DaysWorkedPerWeek.Value;
    }
}










