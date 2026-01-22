namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class ProductTypeRepository(Context context) : GenericDatabaseRepository<ProductTypeDatabaseEntity>(context), IProductTypeRepository
{
    public override async Task<ProductTypeDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(p => p.PublicId == publicId);
}








