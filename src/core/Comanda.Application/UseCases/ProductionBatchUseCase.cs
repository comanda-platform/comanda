namespace Comanda.Application.UseCases;

using Comanda.Application.Notifications;
using Comanda.Application.Notifications.Events;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;
using Comanda.Shared.Enums;

public class ProductionBatchUseCase(
    IProductionBatchRepository batchRepository,
    IProductRepository productRepository,
    IDailyMenuRepository dailyMenuRepository,
    INotificationPublisher notifications) : UseCaseBase("Production batch")
{
    private readonly IProductionBatchRepository _batchRepository = batchRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IDailyMenuRepository _dailyMenuRepository = dailyMenuRepository;
    private readonly INotificationPublisher _notifications = notifications;

    /// <summary>
    /// Start a new production batch for a product
    /// </summary>
    public async Task<ProductionBatch> StartBatchAsync(
        string productPublicId,
        string dailyMenuPublicId,
        DateOnly productionDate,
        string? startedByPublicId = null)
    {
        // Validate product exists
        var product = await _productRepository.GetByPublicIdAsync(productPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Product, productPublicId);

        // Validate daily menu exists
        var dailyMenu = await _dailyMenuRepository.GetByPublicIdAsync(dailyMenuPublicId)
            ?? throw new NotFoundException("Daily menu", dailyMenuPublicId);

        var batch = new ProductionBatch(
            productPublicId,
            dailyMenuPublicId,
            productionDate,
            startedByPublicId);

        await _batchRepository.AddAsync(batch);

        await _notifications.PublishAsync(new BatchStartedEvent(
            batch.PublicId,
            productPublicId,
            product.Name,
            productionDate));

        return batch;
    }

    /// <summary>
    /// Complete a production batch with the yield
    /// </summary>
    public async Task<ProductionBatch> CompleteBatchAsync(
        string batchPublicId,
        int yield,
        string? completedByPublicId = null,
        string? notes = null)
    {
        var batch = await _batchRepository.GetByPublicIdAsync(batchPublicId)
            ?? throw new NotFoundException("Production batch", batchPublicId);

        batch.Complete(yield, completedByPublicId, notes);

        await _batchRepository.UpdateAsync(batch);

        await _notifications.PublishAsync(new BatchCompletedEvent(
            batch.PublicId,
            batch.ProductPublicId,
            yield,
            batch.ProductionDate));

        return batch;
    }

    /// <summary>
    /// Get a batch by its public ID
    /// </summary>
    public async Task<ProductionBatch?> GetBatchByPublicIdAsync(string publicId)
        => await _batchRepository.GetByPublicIdAsync(publicId);

    /// <summary>
    /// Get all batches for a specific date
    /// </summary>
    public async Task<IEnumerable<ProductionBatch>> GetBatchesByDateAsync(DateOnly date)
        => await _batchRepository.GetByDateAsync(date);

    /// <summary>
    /// Get all batches for a specific product
    /// </summary>
    public async Task<IEnumerable<ProductionBatch>> GetBatchesByProductAsync(string productPublicId)
        => await _batchRepository.GetByProductPublicIdAsync(productPublicId);

    /// <summary>
    /// Get all batches for a daily menu
    /// </summary>
    public async Task<IEnumerable<ProductionBatch>> GetBatchesByDailyMenuAsync(string dailyMenuPublicId)
        => await _batchRepository.GetByDailyMenuPublicIdAsync(dailyMenuPublicId);

    /// <summary>
    /// Get all batches for a product on a specific date
    /// </summary>
    public async Task<IEnumerable<ProductionBatch>> GetBatchesByDateAndProductAsync(
        DateOnly date,
        string productPublicId)
        => await _batchRepository.GetByDateAndProductAsync(date, productPublicId);

    /// <summary>
    /// Get total yield for a product on a specific date
    /// </summary>
    public async Task<int> GetTotalYieldByProductAndDateAsync(
        string productPublicId,
        DateOnly date)
        => await _batchRepository.GetTotalYieldByProductAndDateAsync(productPublicId, date);

    /// <summary>
    /// Get all in-progress batches
    /// </summary>
    public async Task<IEnumerable<ProductionBatch>> GetInProgressBatchesAsync()
        => await _batchRepository.GetByStatusAsync(BatchStatus.InProgress);
}







