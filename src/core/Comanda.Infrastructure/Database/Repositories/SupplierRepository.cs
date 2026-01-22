namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class SupplierRepository(Context context) : GenericDatabaseRepository<SupplierDatabaseEntity>(context), ISupplierRepository
{
    public override async Task<SupplierDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(s => s.PublicId == publicId);

    public async Task<IEnumerable<SupplierDatabaseEntity>> GetByTypeAsync(int supplierTypeId) =>
        await Query()
            .Where(s => s.SupplierTypeId == supplierTypeId)
            .ToListAsync();
}







