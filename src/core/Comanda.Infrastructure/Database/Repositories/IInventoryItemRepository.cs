namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IInventoryItemRepository : IGenericDatabaseRepository<InventoryItemDatabaseEntity>
{
    Task<InventoryItemDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<InventoryItemDatabaseEntity>> SearchByNameAsync(string searchTerm);
}







