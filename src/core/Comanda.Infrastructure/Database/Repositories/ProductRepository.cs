namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class ProductRepository(Context context) : GenericDatabaseRepository<ProductDatabaseEntity>(context), IProductRepository
{
    public override async Task<ProductDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(p => p.PublicId == publicId);

    public async Task<IEnumerable<ProductDatabaseEntity>> GetByTypeAsync(ProductTypeDatabaseEntity type)  =>
        await Query()
            .Where(p => p.Type == type)
            .ToListAsync();
}








