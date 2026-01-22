namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Infrastructure.Mappers;

public class ExpenseRepositoryAdapter(
    Database.Repositories.IExpenseRepository databaseRepository,
    Context context) : Domain.Repositories.IExpenseRepository
{
    private readonly Database.Repositories.IExpenseRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Expense?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Expense?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Expense>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Expense>> GetActiveAsync()
    {
        var entities = await _databaseRepository.GetActiveAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Expense>> GetByTypeAsync(ExpenseType type)
    {
        var entities = await _databaseRepository.GetByTypeAsync((int)type);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Expense>> GetByEmployeeIdAsync(int employeeId)
    {
        var entities = await _databaseRepository.GetByEmployeeIdAsync(employeeId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Expense>> GetByLocationIdAsync(int locationId)
    {
        var entities = await _databaseRepository.GetByLocationIdAsync(locationId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Expense>> GetActiveOnDateAsync(DateTime date)
    {
        var entities = await _databaseRepository.GetActiveOnDateAsync(date);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Expense expense)
    {
        var entity = expense.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Expense expense)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(expense.PublicId)
            ?? throw new InvalidOperationException($"Expense with PublicId {expense.PublicId} not found");

        expense.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expense expense)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(expense.PublicId)
            ?? throw new InvalidOperationException($"Expense with PublicId {expense.PublicId} not found");

        await _databaseRepository.DeleteAsync(entity);
    }
}







