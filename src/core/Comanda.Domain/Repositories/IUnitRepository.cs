namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public interface IUnitRepository
{
    Task<Unit?> GetByIdAsync(int id);
    Task<Unit?> GetByPublicIdAsync(string publicId);
    Task<Unit?> GetByCodeAsync(string code);
    Task<IEnumerable<Unit>> GetAllAsync();
    Task<IEnumerable<Unit>> GetByCategoryAsync(UnitCategory category);
    Task AddAsync(Unit unit);
    Task UpdateAsync(Unit unit);
    Task DeleteAsync(Unit unit);
}







