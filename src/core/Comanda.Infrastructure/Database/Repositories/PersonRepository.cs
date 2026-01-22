namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class PersonRepository(Context context)
    : GenericDatabaseRepository<PersonDatabaseEntity>(context), IPersonRepository
{
    public override async Task<PersonDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query()
            .FirstOrDefaultAsync(p => p.PublicId == publicId);
}
