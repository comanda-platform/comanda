namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface IInventoryItemRepository
{
    Task<InventoryItem?> GetByIdAsync(int id);
    Task<InventoryItem?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<InventoryItem>> GetAllAsync();
    Task<IEnumerable<InventoryItem>> SearchByNameAsync(string searchTerm);
    Task AddAsync(InventoryItem item);
    Task UpdateAsync(InventoryItem item);
    Task DeleteAsync(InventoryItem item);
}







