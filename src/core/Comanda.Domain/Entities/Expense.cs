namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;
using Comanda.Domain.Helpers;

public class Expense
{
    public string PublicId { get; private set; }
    public string Description { get; private set; }
    public ExpenseType Type { get; private set; }
    public decimal Amount { get; private set; }
    public ExpenseFrequency Frequency { get; private set; }
    public DateTime EffectiveFrom { get; private set; }
    public DateTime? EffectiveTo { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Payment schedule
    public int? DayOfMonth { get; private set; }
    public DayOfWeek? DayOfWeek { get; private set; }
    public DateTime? SpecificPayableDate { get; private set; }

    // For daily/per-diem calculations
    public int? DaysWorkedPerWeek { get; private set; }
    public bool CalculateDailyRate { get; private set; }

    // Relationships
    internal int? LocationId { get; private set; }
    public string? LocationPublicId { get; private set; }
    internal int? EmployeeId { get; private set; }
    public string? EmployeePublicId { get; private set; }

    private Expense() { } // For reflection / serializers

    private Expense(
        string publicId,
        string description,
        ExpenseType type,
        decimal amount,
        ExpenseFrequency frequency,
        DateTime effectiveFrom,
        DateTime? effectiveTo,
        DateTime createdAt,
        int? dayOfMonth,
        DayOfWeek? dayOfWeek,
        DateTime? specificPayableDate,
        int? daysWorkedPerWeek,
        bool calculateDailyRate,
        string? locationPublicId,
        string? employeePublicId)
    {
        PublicId = publicId;
        Description = description;
        Type = type;
        Amount = amount;
        Frequency = frequency;
        EffectiveFrom = effectiveFrom;
        EffectiveTo = effectiveTo;
        CreatedAt = createdAt;
        DayOfMonth = dayOfMonth;
        DayOfWeek = dayOfWeek;
        SpecificPayableDate = specificPayableDate;
        DaysWorkedPerWeek = daysWorkedPerWeek;
        CalculateDailyRate = calculateDailyRate;
        LocationPublicId = locationPublicId;
        EmployeePublicId = employeePublicId;
    }

    public Expense(
        string description,
        ExpenseType type,
        decimal amount,
        ExpenseFrequency frequency,
        DateTime? effectiveFrom = null,
        int? dayOfMonth = null,
        DayOfWeek? dayOfWeek = null,
        DateTime? specificPayableDate = null,
        int? daysWorkedPerWeek = null,
        bool calculateDailyRate = false,
        string? locationPublicId = null,
        string? employeePublicId = null)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(description, "Description is required");

        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));

        if (dayOfMonth.HasValue && (dayOfMonth.Value < 1 || dayOfMonth.Value > 31))
            throw new ArgumentException("Day of month must be between 1 and 31", nameof(dayOfMonth));

        if (daysWorkedPerWeek.HasValue && (daysWorkedPerWeek.Value < 1 || daysWorkedPerWeek.Value > 7))
            throw new ArgumentException("Days worked per week must be between 1 and 7", nameof(daysWorkedPerWeek));

        // Validate employee salary type must have employee
        if (type == ExpenseType.EmployeeSalary && !string.IsNullOrWhiteSpace(employeePublicId))
            throw new ArgumentException("Employee salary expense must have an associated employee", nameof(employeePublicId));

        PublicId = PublicIdHelper.Generate();
        Description = description;
        Type = type;
        Amount = amount;
        Frequency = frequency;
        EffectiveFrom = effectiveFrom ?? DateTime.UtcNow;
        DayOfMonth = dayOfMonth;
        DayOfWeek = dayOfWeek;
        SpecificPayableDate = specificPayableDate;
        DaysWorkedPerWeek = daysWorkedPerWeek;
        CalculateDailyRate = calculateDailyRate;
        LocationPublicId = locationPublicId;
        EmployeePublicId = employeePublicId;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required", nameof(description));

        Description = description;
    }

    public void UpdateAmount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));

        Amount = amount;
    }

    public void UpdateFrequency(ExpenseFrequency frequency)
    {
        Frequency = frequency;
    }

    public void End(DateTime? effectiveTo = null)
    {
        if (EffectiveTo.HasValue)
            throw new InvalidOperationException("Expense has already been ended");

        EffectiveTo = effectiveTo ?? DateTime.UtcNow;
    }

    public void SetPaymentSchedule(
        int? dayOfMonth = null,
        DayOfWeek? dayOfWeek = null,
        DateTime? specificPayableDate = null)
    {
        if (dayOfMonth.HasValue && (dayOfMonth.Value < 1 || dayOfMonth.Value > 31))
            throw new ArgumentException("Day of month must be between 1 and 31", nameof(dayOfMonth));

        DayOfMonth = dayOfMonth;
        DayOfWeek = dayOfWeek;
        SpecificPayableDate = specificPayableDate;
    }

    public void SetDailyRateCalculation(
        int? daysWorkedPerWeek,
        bool calculateDailyRate)
    {
        if (daysWorkedPerWeek.HasValue && (daysWorkedPerWeek.Value < 1 || daysWorkedPerWeek.Value > 7))
            throw new ArgumentException("Days worked per week must be between 1 and 7", nameof(daysWorkedPerWeek));

        DaysWorkedPerWeek = daysWorkedPerWeek;
        CalculateDailyRate = calculateDailyRate;
    }

    public decimal GetDailyRate()
    {
        if (!CalculateDailyRate || !DaysWorkedPerWeek.HasValue || DaysWorkedPerWeek.Value == 0)
            return Amount;

        return Amount / DaysWorkedPerWeek.Value;
    }

    public bool IsActive() => !EffectiveTo.HasValue || EffectiveTo.Value > DateTime.UtcNow;

    public bool IsActiveOn(DateTime date) =>
        date >= EffectiveFrom && (!EffectiveTo.HasValue || date <= EffectiveTo.Value);

    public static Expense Rehydrate(
        string publicId,
        string description,
        ExpenseType type,
        decimal amount,
        ExpenseFrequency frequency,
        DateTime effectiveFrom,
        DateTime? effectiveTo,
        DateTime createdAt,
        int? dayOfMonth,
        DayOfWeek? dayOfWeek,
        DateTime? specificPayableDate,
        int? daysWorkedPerWeek,
        bool calculateDailyRate,
        string? locationPublicId,
        string? employeePublicId)
        => new (
            publicId,
            description,
            type,
            amount,
            frequency,
            effectiveFrom,
            effectiveTo,
            createdAt,
            dayOfMonth,
            dayOfWeek,
            specificPayableDate,
            daysWorkedPerWeek,
            calculateDailyRate,
            locationPublicId,
            employeePublicId);
}







