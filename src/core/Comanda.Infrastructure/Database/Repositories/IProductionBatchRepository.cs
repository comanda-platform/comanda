namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;
using Comanda.Shared.Enums;

public interface IProductionBatchRepository : IGenericDatabaseRepository<ProductionBatchDatabaseEntity>
{
    Task<ProductionBatchDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByProductIdAsync(int productId);
    Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByProductPublicIdAsync(string productPublicId);
    Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByDailyMenuIdAsync(int dailyMenuId);
    Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByDailyMenuPublicIdAsync(string dailyMenuPublicId);
    Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByDateAsync(DateOnly date);
    Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByDateAndProductIdAsync(DateOnly date, int productId);
    Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByStatusAsync(BatchStatus status);
    Task<int> GetTotalYieldByProductIdAndDateAsync(int productId, DateOnly date);
}







