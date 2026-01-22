namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IExpenseRepository : IGenericDatabaseRepository<ExpenseDatabaseEntity>
{
    Task<ExpenseDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<ExpenseDatabaseEntity>> GetActiveAsync();
    Task<IEnumerable<ExpenseDatabaseEntity>> GetByTypeAsync(int expenseTypeId);
    Task<IEnumerable<ExpenseDatabaseEntity>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<ExpenseDatabaseEntity>> GetByLocationIdAsync(int locationId);
    Task<IEnumerable<ExpenseDatabaseEntity>> GetActiveOnDateAsync(DateTime date);
}







