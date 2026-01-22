namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IDailyMenuRepository : IGenericDatabaseRepository<DailyMenuDatabaseEntity>
{
    Task<DailyMenuDatabaseEntity?> GetByPublicIdAsync(string publicId);

    Task<DailyMenuDatabaseEntity?> GetByDateAsync(
        DateOnly date,
        int? locationId = null,
        string? locationPublicId = null);

    Task<IEnumerable<DailyMenuDatabaseEntity>> GetByDateRangeAsync(
        DateOnly from,
        DateOnly to,
        int? locationId = null,
        string? locationPublicId = null);

    Task<IEnumerable<DailyMenuDatabaseEntity>> GetByLocationIdAsync(int locationId);
}







