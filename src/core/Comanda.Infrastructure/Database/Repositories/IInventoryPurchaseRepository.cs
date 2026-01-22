namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IInventoryPurchaseRepository : IGenericDatabaseRepository<InventoryPurchaseDatabaseEntity>
{
    Task<InventoryPurchaseDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetBySupplierIdAsync(int supplierId);
    Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetBySupplierPublicIdAsync(string supplierPublicId);
    Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetByTypeAsync(int purchaseTypeId);
    Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetPendingDeliveryAsync();
}







