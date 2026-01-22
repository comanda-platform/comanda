namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public interface IExpenseRepository
{
    Task<Expense?> GetByIdAsync(int id);
    Task<Expense?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Expense>> GetAllAsync();
    Task<IEnumerable<Expense>> GetActiveAsync();
    Task<IEnumerable<Expense>> GetByTypeAsync(ExpenseType type);
    Task<IEnumerable<Expense>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<Expense>> GetByLocationIdAsync(int locationId);
    Task<IEnumerable<Expense>> GetActiveOnDateAsync(DateTime date);
    Task AddAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(Expense expense);
}







