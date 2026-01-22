namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public interface IProductionBatchRepository
{
    Task<ProductionBatch?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<ProductionBatch>> GetByProductPublicIdAsync(string productPublicId);
    Task<IEnumerable<ProductionBatch>> GetByDailyMenuPublicIdAsync(string dailyMenuPublicId);
    Task<IEnumerable<ProductionBatch>> GetByDateAsync(DateOnly date);
    Task<IEnumerable<ProductionBatch>> GetByDateAndProductAsync(DateOnly date, string productPublicId);
    Task<IEnumerable<ProductionBatch>> GetByStatusAsync(BatchStatus status);
    Task<int> GetTotalYieldByProductAndDateAsync(string productPublicId, DateOnly date);
    Task AddAsync(ProductionBatch batch);
    Task UpdateAsync(ProductionBatch batch);
}







