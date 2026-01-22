namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class InventoryItemRepositoryAdapter(
    Database.Repositories.IInventoryItemRepository databaseRepository,
    Context context) : Domain.Repositories.IInventoryItemRepository
{
    private readonly Database.Repositories.IInventoryItemRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<InventoryItem?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<InventoryItem?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<InventoryItem>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<InventoryItem>> SearchByNameAsync(string searchTerm)
    {
        var entities = await _databaseRepository.SearchByNameAsync(searchTerm);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(InventoryItem item)
    {
        var entity = item.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(InventoryItem item)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(item.PublicId)

            ?? throw new InvalidOperationException($"InventoryItem with PublicId {item.PublicId} not found");

        item.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(InventoryItem item)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(item.PublicId)
            ?? throw new InvalidOperationException($"InventoryItem with PublicId {item.PublicId} not found");

        await _databaseRepository.DeleteAsync(entity);
    }
}







