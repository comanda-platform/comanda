namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class EmployeeRepositoryAdapter(
    Database.Repositories.IEmployeeRepository databaseRepository,
    Context context) : Domain.Repositories.IEmployeeRepository
{
    private readonly Database.Repositories.IEmployeeRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Employee?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Employee?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<Employee?> GetByUserNameAsync(string userName)
    {
        var entity = await _databaseRepository.GetByUserNameAsync(userName);

        return entity?.FromPersistence();
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        var entity = await _databaseRepository.GetByEmailAsync(email);

        return entity?.FromPersistence();
    }

    public async Task<Employee?> GetByApiKeyAsync(string apiKey)
    {
        var entity = await _databaseRepository.GetByApiKeyAsync(apiKey);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
    {
        var entities = await _databaseRepository.GetActiveEmployeesAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<bool> ExistsByUserNameAsync(string userName)
    {
        return await _databaseRepository.ExistsByUserNameAsync(userName);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _databaseRepository.ExistsByEmailAsync(email);
    }

    public async Task AddAsync(Employee employee)
    {
        var entity = employee.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Employee employee)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(employee.PublicId)
            ?? throw new InvalidOperationException($"Employee with PublicId {employee.PublicId} not found");

        employee.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Employee employee)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(employee.PublicId)
            ?? throw new InvalidOperationException($"Employee with PublicId {employee.PublicId} not found");

        await _databaseRepository.DeleteAsync(entity);
    }
}






