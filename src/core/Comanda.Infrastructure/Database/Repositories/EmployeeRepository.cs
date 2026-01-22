namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class EmployeeRepository(Context context)
    : GenericDatabaseRepository<EmployeeDatabaseEntity>(context), IEmployeeRepository
{
    public override async Task<EmployeeDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(e => e.PublicId == publicId);

    public async Task<EmployeeDatabaseEntity?> GetByUserNameAsync(string userName) =>
        await Query().FirstOrDefaultAsync(e => e.UserName == userName);

    public async Task<EmployeeDatabaseEntity?> GetByEmailAsync(string email) =>
        await Query().FirstOrDefaultAsync(e => e.Email == email);

    public async Task<EmployeeDatabaseEntity?> GetByApiKeyAsync(string apiKey) =>
        await Query().FirstOrDefaultAsync(e => e.ApiKey == apiKey);

    public async Task<IEnumerable<EmployeeDatabaseEntity>> GetActiveEmployeesAsync() =>
        await Query()
            .Where(e => !e.LockoutEnabled)
            .ToListAsync();

    public async Task<bool> ExistsByUserNameAsync(string userName) => 
        await Query().AnyAsync(e => e.UserName == userName);

    public async Task<bool> ExistsByEmailAsync(string email) =>
        await Query().AnyAsync(e => e.Email == email);
}






