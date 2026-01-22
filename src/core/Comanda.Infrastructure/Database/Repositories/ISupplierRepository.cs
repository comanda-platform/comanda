namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface ISupplierRepository : IGenericDatabaseRepository<SupplierDatabaseEntity>
{
    Task<SupplierDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<SupplierDatabaseEntity>> GetByTypeAsync(int supplierTypeId);
}







