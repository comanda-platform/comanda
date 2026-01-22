namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IProductRepository : IGenericDatabaseRepository<ProductDatabaseEntity>
{
    Task<ProductDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<ProductDatabaseEntity>> GetByTypeAsync(ProductTypeDatabaseEntity type);
}







