namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee?> GetByPublicIdAsync(string publicId);
    Task<Employee?> GetByUserNameAsync(string userName);
    Task<Employee?> GetByEmailAsync(string email);
    Task<Employee?> GetByApiKeyAsync(string apiKey);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
    Task<bool> ExistsByUserNameAsync(string userName);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Employee employee);
}






