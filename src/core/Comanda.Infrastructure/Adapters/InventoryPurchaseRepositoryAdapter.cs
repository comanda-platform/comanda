namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Infrastructure.Mappers;

public class InventoryPurchaseRepositoryAdapter(
    Database.Repositories.IInventoryPurchaseRepository databaseRepository,
    Context context) : Domain.Repositories.IInventoryPurchaseRepository
{
    private readonly Database.Repositories.IInventoryPurchaseRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<InventoryPurchase?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<InventoryPurchase?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<InventoryPurchase>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<InventoryPurchase>> GetBySupplierIdAsync(int supplierId)
    {
        var entities = await _databaseRepository.GetBySupplierIdAsync(supplierId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<InventoryPurchase>> GetBySupplierPublicIdAsync(string supplierPublicId)
    {
        var entities = await _databaseRepository.GetBySupplierPublicIdAsync(supplierPublicId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<InventoryPurchase>> GetByTypeAsync(InventoryPurchaseType type)
    {
        var entities = await _databaseRepository.GetByTypeAsync((int)type);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<InventoryPurchase>> GetByDateRangeAsync(DateTime from, DateTime to)
    {
        var entities = await _databaseRepository.GetByDateRangeAsync(
            from,
            to);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<InventoryPurchase>> GetPendingDeliveryAsync()
    {
        var entities = await _databaseRepository.GetPendingDeliveryAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(InventoryPurchase purchase)
    {
        var entity = purchase.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(InventoryPurchase purchase)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(purchase.PublicId)

            ?? throw new InvalidOperationException($"Inventory purchase with PublicId {purchase.PublicId} not found");

        purchase.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(InventoryPurchase purchase)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(purchase.PublicId)
            ?? throw new InvalidOperationException($"Inventory purchase with PublicId {purchase.PublicId} not found");

        await _databaseRepository.DeleteAsync(entity);
    }
}







