namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IProductTypeRepository : IGenericDatabaseRepository<ProductTypeDatabaseEntity> 
{
    Task<ProductTypeDatabaseEntity?> GetByPublicIdAsync(string publicId);
}







