namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;
using Comanda.Shared.Enums;

public class ProductionBatchRepository(Context context)
    : GenericDatabaseRepository<ProductionBatchDatabaseEntity>(context), IProductionBatchRepository
{
    public override async Task<ProductionBatchDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(b => b.PublicId == publicId);

    public async Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByProductIdAsync(int productId) =>
        await Query()
            .Where(b => b.ProductId == productId)
            .OrderByDescending(b => b.StartedAt)
            .ToListAsync();

    public async Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByProductPublicIdAsync(string productPublicId) =>
        await Query()
            .Where(b => b.Product.PublicId == productPublicId)
            .OrderByDescending(b => b.StartedAt)
            .ToListAsync();

    public async Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByDailyMenuIdAsync(int dailyMenuId) =>
        await Query()
            .Where(b => b.DailyMenuId == dailyMenuId)
            .OrderByDescending(b => b.StartedAt)
            .ToListAsync();

    public async Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByDailyMenuPublicIdAsync(string dailyMenuPublicId) =>
        await Query()
            .Where(b => b.DailyMenu.PublicId == dailyMenuPublicId)
            .OrderByDescending(b => b.StartedAt)
            .ToListAsync();

    public async Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByDateAsync(DateOnly date) =>
        await Query()
            .Where(b => b.ProductionDate == date)
            .OrderByDescending(b => b.StartedAt)
            .ToListAsync();

    public async Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByDateAndProductIdAsync(DateOnly date, int productId) =>
        await Query()
            .Where(b => b.ProductionDate == date && b.ProductId == productId)
            .OrderByDescending(b => b.StartedAt)
            .ToListAsync();

    public async Task<IEnumerable<ProductionBatchDatabaseEntity>> GetByStatusAsync(BatchStatus status) =>
        await Query()
            .Where(b => b.Status == status)
            .OrderByDescending(b => b.StartedAt)
            .ToListAsync();

    public async Task<int> GetTotalYieldByProductIdAndDateAsync(int productId, DateOnly date) =>
        await Query()
            .Where(b => b.ProductId == productId
                && b.ProductionDate == date
                && b.Status == BatchStatus.Completed
                && b.Yield.HasValue)
            .SumAsync(b => b.Yield!.Value);
}







