namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IUnitRepository : IGenericDatabaseRepository<UnitDatabaseEntity>
{
    Task<UnitDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<UnitDatabaseEntity?> GetByCodeAsync(string code);
    Task<IEnumerable<UnitDatabaseEntity>> GetByCategoryAsync(int categoryId);
}







