namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class UnitRepository(Context context) : GenericDatabaseRepository<UnitDatabaseEntity>(context), IUnitRepository
{
    public override async Task<UnitDatabaseEntity?> GetByPublicIdAsync(string publicId) => 
        await Query().FirstOrDefaultAsync(u => u.PublicId == publicId);

    public async Task<UnitDatabaseEntity?> GetByCodeAsync(string code) =>
        await Query().FirstOrDefaultAsync(u => u.Code == code);

    public async Task<IEnumerable<UnitDatabaseEntity>> GetByCategoryAsync(int categoryId) =>
        await Query()
            .Where(u => u.CategoryId == categoryId)
            .ToListAsync();
}







