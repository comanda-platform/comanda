namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class SideRepository(Context context) : GenericDatabaseRepository<SideDatabaseEntity>(context), ISideRepository
{
    public override async Task<SideDatabaseEntity?> GetByPublicIdAsync(string publicId) => 
        await Query().FirstOrDefaultAsync(s => s.PublicId == publicId);

    public async Task<IEnumerable<SideDatabaseEntity>> GetActiveAsync() =>
        await Query()
            .Where(s => s.IsActive)
            .ToListAsync();
}







