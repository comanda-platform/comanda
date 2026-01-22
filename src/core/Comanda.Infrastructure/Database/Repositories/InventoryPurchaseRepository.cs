namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class InventoryPurchaseRepository(Context context) : GenericDatabaseRepository<InventoryPurchaseDatabaseEntity>(context), IInventoryPurchaseRepository
{
    public override async Task<InventoryPurchaseDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(p => p.PublicId == publicId);

    public async Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetBySupplierIdAsync(int supplierId) =>
        await Query()
            .Where(p => p.SupplierId == supplierId)
            .ToListAsync();

    public async Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetBySupplierPublicIdAsync(string supplierPublicId) =>
        await Query()
            .Where(p => p.Supplier.PublicId == supplierPublicId)
            .ToListAsync();

    public async Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetByTypeAsync(int purchaseTypeId) =>
        await Query()
            .Where(p => p.PurchaseTypeId == purchaseTypeId)
            .ToListAsync();

    public async Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetByDateRangeAsync(
        DateTime from,
        DateTime to) =>
        await Query()
            .Where(p => p.PurchasedAt >= from && p.PurchasedAt <= to)
            .ToListAsync();

    public async Task<IEnumerable<InventoryPurchaseDatabaseEntity>> GetPendingDeliveryAsync() =>
        await Query()
            .Where(p => p.DeliveredAt == null)
            .ToListAsync();
}







