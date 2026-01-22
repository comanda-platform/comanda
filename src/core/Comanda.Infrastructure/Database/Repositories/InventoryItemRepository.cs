namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class InventoryItemRepository(Context context) : GenericDatabaseRepository<InventoryItemDatabaseEntity>(context), IInventoryItemRepository
{
    public override async Task<InventoryItemDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(i => i.PublicId == publicId);

    public async Task<IEnumerable<InventoryItemDatabaseEntity>> SearchByNameAsync(string searchTerm) =>
        await Query().Where(i => i.Name.Contains(searchTerm)).ToListAsync();
}







