namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class ExpenseRepository(Context context) : GenericDatabaseRepository<ExpenseDatabaseEntity>(context), IExpenseRepository
{
    public override async Task<ExpenseDatabaseEntity?> GetByPublicIdAsync(string publicId) => 
        await Query().FirstOrDefaultAsync(e => e.PublicId == publicId);

    public async Task<IEnumerable<ExpenseDatabaseEntity>> GetActiveAsync() =>
        await Query().Where(e => e.EffectiveTo == null).ToListAsync();

    public async Task<IEnumerable<ExpenseDatabaseEntity>> GetByTypeAsync(int expenseTypeId) =>
        await Query()
            .Where(e => e.ExpenseTypeId == expenseTypeId)
            .ToListAsync();

    public async Task<IEnumerable<ExpenseDatabaseEntity>> GetByEmployeeIdAsync(int employeeId) => 
        await Query()
            .Where(e => e.EmployeeId == employeeId)
            .ToListAsync();

    public async Task<IEnumerable<ExpenseDatabaseEntity>> GetByLocationIdAsync(int locationId) => 
        await Query()
            .Where(e => e.LocationId == locationId)
            .ToListAsync();
        
    public async Task<IEnumerable<ExpenseDatabaseEntity>> GetActiveOnDateAsync(DateTime date) =>
        await Query()
            .Where(e => e.EffectiveFrom <= date && (e.EffectiveTo == null || e.EffectiveTo >= date))
            .ToListAsync();
}







