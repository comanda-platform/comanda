namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IClientGroupRepository : IGenericDatabaseRepository<ClientGroupDatabaseEntity>
{
    Task<ClientGroupDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<ClientGroupDatabaseEntity>> GetWithCreditLineAsync();
}







