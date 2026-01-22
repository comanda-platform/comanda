namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public interface ISupplierRepository
{
    Task<Supplier?> GetByIdAsync(int id);
    Task<Supplier?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Supplier>> GetAllAsync();
    Task<IEnumerable<Supplier>> GetByTypeAsync(SupplierType type);
    Task AddAsync(Supplier supplier);
    Task UpdateAsync(Supplier supplier);
    Task DeleteAsync(Supplier supplier);
}







