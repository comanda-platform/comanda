namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class ProductionBatchMapper
{
    extension(ProductionBatchDatabaseEntity entity)
    {
        public ProductionBatch FromPersistence() =>
            ProductionBatch.Rehydrate(
                entity.PublicId,
                entity.Product.PublicId,
                entity.DailyMenu.PublicId,
                entity.ProductionDate,
                entity.Status,
                entity.StartedAt,
                entity.CompletedAt,
                entity.Yield,
                entity.StartedBy?.PublicId,
                entity.CompletedBy?.PublicId,
                entity.Notes);
    }

    public static ProductionBatchDatabaseEntity ToPersistence(this ProductionBatch batch)
    {
        // Note: ProductId and DailyMenuId are set in the repository adapter
        // We use placeholder values here that will be overwritten
        return new ProductionBatchDatabaseEntity
        {
            PublicId = batch.PublicId,
            ProductId = 0, // Set in repository adapter
            DailyMenuId = 0, // Set in repository adapter
            ProductionDate = batch.ProductionDate,
            Status = batch.Status,
            StartedAt = batch.StartedAt,
            CompletedAt = batch.CompletedAt,
            Yield = batch.Yield,
            Notes = batch.Notes
        };
    }

    public static void UpdatePersistence(
        this ProductionBatch batch,
        ProductionBatchDatabaseEntity entity)
    {
        entity.Status = batch.Status;
        entity.CompletedAt = batch.CompletedAt;
        entity.Yield = batch.Yield;
        entity.Notes = batch.Notes;
        // CompletedById is set in the repository
    }
}







