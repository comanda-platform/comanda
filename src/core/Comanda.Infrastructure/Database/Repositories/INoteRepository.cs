namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface INoteRepository : IGenericDatabaseRepository<NoteDatabaseEntity>
{
    Task<NoteDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<NoteDatabaseEntity>> GetByClientPublicIdAsync(string clientPublicId);
    Task<IEnumerable<NoteDatabaseEntity>> GetByClientGroupPublicIdAsync(string clientGroupPublicId);
    Task<IEnumerable<NoteDatabaseEntity>> GetByLocationPublicIdAsync(string locationPublicId);
    Task<IEnumerable<NoteDatabaseEntity>> GetByOrderPublicIdAsync(string orderPublicId);
    Task<IEnumerable<NoteDatabaseEntity>> GetByOrderLinePublicIdAsync(string orderLinePublicId);
    Task<IEnumerable<NoteDatabaseEntity>> GetByProductPublicIdAsync(string productPublicId);
    Task<IEnumerable<NoteDatabaseEntity>> GetBySidePublicIdAsync(string sidePublicId);
}







