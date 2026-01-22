namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class ClientRepository(Context context)
    : GenericDatabaseRepository<ClientDatabaseEntity>(context), IClientRepository
{
    public override async Task<ClientDatabaseEntity?> GetByPublicIdAsync(string publicId) => 
        await Query()
            .FirstOrDefaultAsync(c => c.PublicId == publicId);

    public async Task<IEnumerable<ClientDatabaseEntity>> GetByGroupIdAsync(int clientGroupId) => 
        await Query()
            .Where(c => c.ClientGroupId == clientGroupId)
            .ToListAsync();

    public async Task<IEnumerable<ClientDatabaseEntity>> GetByGroupPublicIdAsync(string clientGroupPublicId) =>
            await Query()
                .Where(c => 
                    c.Group != null 
                    && c.Group.PublicId == clientGroupPublicId)
                .ToListAsync();
}







