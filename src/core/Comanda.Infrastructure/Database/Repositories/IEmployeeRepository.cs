namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IEmployeeRepository : IGenericDatabaseRepository<EmployeeDatabaseEntity>
{
    Task<EmployeeDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<EmployeeDatabaseEntity?> GetByUserNameAsync(string userName);
    Task<EmployeeDatabaseEntity?> GetByEmailAsync(string email);
    Task<EmployeeDatabaseEntity?> GetByApiKeyAsync(string apiKey);
    Task<IEnumerable<EmployeeDatabaseEntity>> GetActiveEmployeesAsync();
    Task<bool> ExistsByUserNameAsync(string userName);
    Task<bool> ExistsByEmailAsync(string email);
}






