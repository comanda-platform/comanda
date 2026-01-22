namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface ISideRepository : IGenericDatabaseRepository<SideDatabaseEntity>
{
    Task<SideDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<SideDatabaseEntity>> GetActiveAsync();
}







