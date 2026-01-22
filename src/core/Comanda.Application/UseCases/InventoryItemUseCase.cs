namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class InventoryItemUseCase(
    IInventoryItemRepository inventoryItemRepository,
    IUnitRepository unitRepository) : UseCaseBase(EntityTypePrintNames.InventoryItem)
{
    private readonly IInventoryItemRepository _inventoryItemRepository = inventoryItemRepository;
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<InventoryItem> CreateAsync(
        string name,
        string baseUnitPublicId)
    {
        var baseUnit = await _unitRepository.GetByPublicIdAsync(baseUnitPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Unit, baseUnitPublicId);

        var item = new InventoryItem(name, baseUnit);

        await _inventoryItemRepository.AddAsync(item);

        return item;
    }

    public async Task<InventoryItem> GetByPublicIdAsync(string publicId)
        => await _inventoryItemRepository.GetByPublicIdAsync(publicId)
        ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<IEnumerable<InventoryItem>> GetAllAsync()
        => await _inventoryItemRepository.GetAllAsync();

    public async Task<IEnumerable<InventoryItem>> SearchByNameAsync(string searchTerm)
        => await _inventoryItemRepository.SearchByNameAsync(searchTerm);

    public async Task UpdateNameAsync(string publicId, string name)
    {
        var item = await _inventoryItemRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        item.UpdateName(name);

        await _inventoryItemRepository.UpdateAsync(item);
    }

    public async Task UpdateBaseUnitAsync(string publicId, string baseUnitPublicId)
    {
        var item = await _inventoryItemRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        var baseUnit = await _unitRepository.GetByPublicIdAsync(baseUnitPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Unit, baseUnitPublicId);

        item.UpdateBaseUnit(baseUnit);

        await _inventoryItemRepository.UpdateAsync(item);
    }

    public async Task DeleteAsync(string publicId)
    {
        var item = await _inventoryItemRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _inventoryItemRepository.DeleteAsync(item);
    }
}







