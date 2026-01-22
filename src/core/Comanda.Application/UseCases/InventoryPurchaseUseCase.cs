namespace Comanda.Application.UseCases;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Domain.Repositories;
using Comanda.Domain;

public class InventoryPurchaseUseCase(
    IInventoryPurchaseRepository inventoryPurchaseRepository,
    ISupplierRepository supplierRepository,
    IInventoryItemRepository inventoryItemRepository,
    IUnitRepository unitRepository) : UseCaseBase(EntityTypePrintNames.InventoryPurchase)
{
    private readonly IInventoryPurchaseRepository _inventoryPurchaseRepository = inventoryPurchaseRepository;
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    private readonly IInventoryItemRepository _inventoryItemRepository = inventoryItemRepository;
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<InventoryPurchase> CreateAsync(
        string supplierPublicId,
        InventoryPurchaseType purchaseType,
        DateTime? purchasedAt = null,
        string? storeLocationPublicId = null)
    {
        var supplier = await _supplierRepository.GetByPublicIdAsync(supplierPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Supplier, supplierPublicId);

        var purchase = new InventoryPurchase(
            supplier,
            purchaseType,
            purchasedAt,
            storeLocationPublicId);

        await _inventoryPurchaseRepository.AddAsync(purchase);

        return purchase;
    }

    public async Task<InventoryPurchase> GetByPublicIdAsync(string publicId)
        => await _inventoryPurchaseRepository.GetByPublicIdAsync(publicId)
        ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<IEnumerable<InventoryPurchase>> GetAllAsync()
        => await _inventoryPurchaseRepository.GetAllAsync();

    public async Task<IEnumerable<InventoryPurchase>> GetBySupplierAsync(string supplierPublicId)
    {
        var supplier = await _supplierRepository.GetByPublicIdAsync(supplierPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Supplier, supplierPublicId);

        return await _inventoryPurchaseRepository.GetBySupplierPublicIdAsync(supplier.PublicId);
    }

    public async Task<IEnumerable<InventoryPurchase>> GetByTypeAsync(InventoryPurchaseType type)
        => await _inventoryPurchaseRepository.GetByTypeAsync(type);

    public async Task<IEnumerable<InventoryPurchase>> GetByDateRangeAsync(DateTime from, DateTime to)
        => await _inventoryPurchaseRepository.GetByDateRangeAsync(from, to);

    public async Task<IEnumerable<InventoryPurchase>> GetPendingDeliveryAsync()
        => await _inventoryPurchaseRepository.GetPendingDeliveryAsync();

    public async Task AddLineAsync(
        string purchasePublicId,
        string inventoryItemPublicId,
        decimal quantity,
        decimal unitPrice,
        string unitPublicId)
    {
        var purchase = await _inventoryPurchaseRepository.GetByPublicIdAsync(purchasePublicId)
            ?? throw new NotFoundException(
                EntityTypePrintName,
                purchasePublicId);

        var item = await _inventoryItemRepository.GetByPublicIdAsync(inventoryItemPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.InventoryItem, inventoryItemPublicId);

        var unit = await _unitRepository.GetByPublicIdAsync(unitPublicId)
            ?? throw new NotFoundException(
                EntityTypePrintNames.Unit,
                unitPublicId);

        var line = new InventoryPurchaseLine(
            item,
            quantity,
            unitPrice,
            unit);

        purchase.AddLine(line);

        await _inventoryPurchaseRepository.UpdateAsync(purchase);
    }

    public async Task MarkAsDeliveredAsync(string publicId, DateTime? deliveredAt = null)
    {
        var purchase = await _inventoryPurchaseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        purchase.MarkAsDelivered(deliveredAt);

        await _inventoryPurchaseRepository.UpdateAsync(purchase);
    }

    public async Task UpdateTypeAsync(string publicId, InventoryPurchaseType purchaseType)
    {
        var purchase = await _inventoryPurchaseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        purchase.UpdatePurchaseType(purchaseType);

        await _inventoryPurchaseRepository.UpdateAsync(purchase);
    }

    public async Task DeleteAsync(string publicId)
    {
        var purchase = await _inventoryPurchaseRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _inventoryPurchaseRepository.DeleteAsync(purchase);
    }
}







