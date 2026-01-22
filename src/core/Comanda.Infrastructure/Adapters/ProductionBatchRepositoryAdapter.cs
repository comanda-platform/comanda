namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Infrastructure.Mappers;
using Comanda.Domain;

public class ProductionBatchRepositoryAdapter(
    Database.Repositories.IProductionBatchRepository databaseRepository,
    Context context) : Domain.Repositories.IProductionBatchRepository
{
    private readonly Database.Repositories.IProductionBatchRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<ProductionBatch?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);
        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<ProductionBatch>> GetByProductPublicIdAsync(string productPublicId)
    {
        var entities = await _databaseRepository.GetByProductPublicIdAsync(productPublicId);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<ProductionBatch>> GetByDailyMenuPublicIdAsync(string dailyMenuPublicId)
    {
        var entities = await _databaseRepository.GetByDailyMenuPublicIdAsync(dailyMenuPublicId);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<ProductionBatch>> GetByDateAsync(DateOnly date)
    {
        var entities = await _databaseRepository.GetByDateAsync(date);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<ProductionBatch>> GetByDateAndProductAsync(DateOnly date, string productPublicId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.PublicId == productPublicId);
        if (product == null)
            return [];

        var entities = await _databaseRepository.GetByDateAndProductIdAsync(date, product.Id);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<ProductionBatch>> GetByStatusAsync(BatchStatus status)
    {
        var entities = await _databaseRepository.GetByStatusAsync(status);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<int> GetTotalYieldByProductAndDateAsync(string productPublicId, DateOnly date)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.PublicId == productPublicId);
        if (product == null)
            return 0;

        return await _databaseRepository.GetTotalYieldByProductIdAndDateAsync(product.Id, date);
    }

    public async Task AddAsync(ProductionBatch batch)
    {
        var entity = batch.ToPersistence();

        // Resolve Product
        var product = await _context.Products.FirstOrDefaultAsync(p => p.PublicId == batch.ProductPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Product, batch.ProductPublicId);
        entity.ProductId = product.Id;

        // Resolve DailyMenu
        var dailyMenu = await _context.DailyMenus.FirstOrDefaultAsync(dm => dm.PublicId == batch.DailyMenuPublicId)
            ?? throw new NotFoundException("Daily menu", batch.DailyMenuPublicId);
        entity.DailyMenuId = dailyMenu.Id;

        // Resolve StartedBy if provided
        if (!string.IsNullOrEmpty(batch.StartedByPublicId))
        {
            var startedBy = await _context.Employees.FirstOrDefaultAsync(e => e.PublicId == batch.StartedByPublicId);
            entity.StartedById = startedBy?.Id;
        }

        await _databaseRepository.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductionBatch batch)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(batch.PublicId)
            ?? throw new NotFoundException("Production batch", batch.PublicId);

        batch.UpdatePersistence(entity);

        // Resolve CompletedBy if provided
        if (!string.IsNullOrEmpty(batch.CompletedByPublicId))
        {
            var completedBy = await _context.Employees.FirstOrDefaultAsync(e => e.PublicId == batch.CompletedByPublicId);
            entity.CompletedById = completedBy?.Id;
        }

        await _context.SaveChangesAsync();
    }
}







