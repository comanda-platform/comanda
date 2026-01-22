namespace Comanda.Api.Mappers;

using Comanda.Api.Models;
using Comanda.Domain.Entities;

public static class ProductionBatchResponseMapper
{
    public static ProductionBatchResponse ToResponse(ProductionBatch batch)
    {
        return new ProductionBatchResponse(
            batch.PublicId,
            batch.ProductPublicId,
            batch.DailyMenuPublicId,
            batch.ProductionDate,
            batch.Status,
            batch.StartedAt,
            batch.CompletedAt,
            batch.Yield,
            batch.StartedByPublicId,
            batch.CompletedByPublicId,
            batch.Notes);
    }
}







