namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public interface IInventoryPurchaseRepository
{
    Task<InventoryPurchase?> GetByIdAsync(int id);
    Task<InventoryPurchase?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<InventoryPurchase>> GetAllAsync();
    Task<IEnumerable<InventoryPurchase>> GetBySupplierIdAsync(int supplierId);
    Task<IEnumerable<InventoryPurchase>> GetBySupplierPublicIdAsync(string supplierPublicId);
    Task<IEnumerable<InventoryPurchase>> GetByTypeAsync(InventoryPurchaseType type);
    Task<IEnumerable<InventoryPurchase>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task<IEnumerable<InventoryPurchase>> GetPendingDeliveryAsync();
    Task AddAsync(InventoryPurchase purchase);
    Task UpdateAsync(InventoryPurchase purchase);
    Task DeleteAsync(InventoryPurchase purchase);
}







