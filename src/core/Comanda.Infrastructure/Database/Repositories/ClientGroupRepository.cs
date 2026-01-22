namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class ClientGroupRepository(Context context)
    : GenericDatabaseRepository<ClientGroupDatabaseEntity>(context), IClientGroupRepository
{
    public override async Task<ClientGroupDatabaseEntity?> GetByPublicIdAsync(string publicId) => 
        await Query()
            .FirstOrDefaultAsync(g => g.PublicId == publicId);

    public async Task<IEnumerable<ClientGroupDatabaseEntity>> GetWithCreditLineAsync() =>
        await Query()
            .Where(g => g.HasCreditLine)
            .ToListAsync();
}







