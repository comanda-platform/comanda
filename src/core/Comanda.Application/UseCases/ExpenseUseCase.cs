namespace Comanda.Application.UseCases;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Domain.Repositories;
using Comanda.Domain;

public class ExpenseUseCase(IExpenseRepository expenseRepository) : UseCaseBase(EntityTypePrintNames.Expense)
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;

    public async Task<Expense> CreateAsync(
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
        var expense = new Expense(
            description,
            type,
            amount,
            frequency,
            effectiveFrom,
            dayOfMonth,
            dayOfWeek,
            specificPayableDate,
            daysWorkedPerWeek,
            calculateDailyRate,
            locationPublicId,
            employeePublicId);

        await _expenseRepository.AddAsync(expense);
        return expense;
    }

    public async Task<Expense> GetByPublicIdAsync(string publicId)
        => await _expenseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<IEnumerable<Expense>> GetAllAsync()
        => await _expenseRepository.GetAllAsync();

    public async Task<IEnumerable<Expense>> GetActiveAsync()
        => await _expenseRepository.GetActiveAsync();

    public async Task<IEnumerable<Expense>> GetByTypeAsync(ExpenseType type)
        => await _expenseRepository.GetByTypeAsync(type);

    public async Task<IEnumerable<Expense>> GetByEmployeeAsync(int employeeId)
        => await _expenseRepository.GetByEmployeeIdAsync(employeeId);

    public async Task<IEnumerable<Expense>> GetByLocationAsync(int locationId)
        => await _expenseRepository.GetByLocationIdAsync(locationId);

    public async Task<IEnumerable<Expense>> GetActiveOnDateAsync(DateTime date)
        => await _expenseRepository.GetActiveOnDateAsync(date);

    public async Task UpdateDescriptionAsync(string publicId, string description)
    {
        var expense = await _expenseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        expense.UpdateDescription(description);

        await _expenseRepository.UpdateAsync(expense);
    }

    public async Task UpdateAmountAsync(string publicId, decimal amount)
    {
        var expense = await _expenseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        expense.UpdateAmount(amount);

        await _expenseRepository.UpdateAsync(expense);
    }

    public async Task UpdateFrequencyAsync(string publicId, ExpenseFrequency frequency)
    {
        var expense = await _expenseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        expense.UpdateFrequency(frequency);

        await _expenseRepository.UpdateAsync(expense);
    }

    public async Task SetPaymentScheduleAsync(
        string publicId,
        int? dayOfMonth = null,
        DayOfWeek? dayOfWeek = null,
        DateTime? specificPayableDate = null)
    {
        var expense = await _expenseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        expense.SetPaymentSchedule(dayOfMonth, dayOfWeek, specificPayableDate);

        await _expenseRepository.UpdateAsync(expense);
    }

    public async Task SetDailyRateCalculationAsync(
        string publicId,
        int? daysWorkedPerWeek,
        bool calculateDailyRate)
    {
        var expense = await _expenseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        expense.SetDailyRateCalculation(daysWorkedPerWeek, calculateDailyRate);

        await _expenseRepository.UpdateAsync(expense);
    }

    public async Task EndAsync(string publicId, DateTime? effectiveTo = null)
    {
        var expense = await _expenseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        expense.End(effectiveTo);

        await _expenseRepository.UpdateAsync(expense);
    }

    public async Task DeleteAsync(string publicId)
    {
        var expense = await _expenseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _expenseRepository.DeleteAsync(expense);
    }
}







