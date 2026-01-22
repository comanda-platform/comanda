namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IClientRepository : IGenericDatabaseRepository<ClientDatabaseEntity>
{
    Task<ClientDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<ClientDatabaseEntity>> GetByGroupIdAsync(int clientGroupId);
    Task<IEnumerable<ClientDatabaseEntity>> GetByGroupPublicIdAsync(string clientGroupPublicId);
}







