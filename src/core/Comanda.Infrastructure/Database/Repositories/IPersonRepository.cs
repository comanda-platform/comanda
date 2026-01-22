namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IPersonRepository : IGenericDatabaseRepository<PersonDatabaseEntity>
{
    Task<PersonDatabaseEntity?> GetByPublicIdAsync(string publicId);
}
