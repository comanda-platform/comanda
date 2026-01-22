namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;

public static class ProductionBatchEndpoints
{
    public static void MapProductionBatchEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/batches")
            .WithTags("Production Batches")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get production batches with optional filters")
            .WithDescription("Retrieves production batches. Use query parameters to filter: date, productId, dailyMenuId, inProgress");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get production batch by ID");

        group.MapGet("/yield/{productPublicId}/{date}", GetTotalYieldAsync)
            .AddEndpointFilter(new RequireNonEmptyStringFilter(0, "productPublicId"))
            .WithSummary("Get total yield for a product on a specific date");
        #endregion

        #region POST
        group.MapPost("/", StartBatchAsync)
            .WithSummary("Start a new production batch");

        group.MapPost("/{publicId}/complete", CompleteBatchAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Complete a production batch");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] ProductionBatchQueryParameters query,
        ProductionBatchUseCase useCase)
    {
        IEnumerable<Domain.Entities.ProductionBatch> batches;

        // Apply filters based on query parameters
        if (query.Date.HasValue)
        {
            batches = await useCase.GetBatchesByDateAsync(query.Date.Value);
        }
        else if (!string.IsNullOrEmpty(query.ProductId))
        {
            batches = await useCase.GetBatchesByProductAsync(query.ProductId);
        }
        else if (!string.IsNullOrEmpty(query.DailyMenuId))
        {
            batches = await useCase.GetBatchesByDailyMenuAsync(query.DailyMenuId);
        }
        else if (query.InProgress.HasValue && query.InProgress.Value)
        {
            batches = await useCase.GetInProgressBatchesAsync();
        }
        else
        {
            // Return empty if no valid filter (or implement GetAllAsync if needed)
            batches = Array.Empty<Domain.Entities.ProductionBatch>();
        }

        return Results.Ok(batches.Select(ProductionBatchResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        ProductionBatchUseCase useCase)
    {
        var batch = await useCase.GetBatchByPublicIdAsync(publicId);

        if (batch == null)
            return Results.NotFound();

        return Results.Ok(ProductionBatchResponseMapper.ToResponse(batch));
    }

    private static async Task<IResult> GetTotalYieldAsync(
        string productPublicId,
        DateOnly date,
        ProductionBatchUseCase useCase)
    {
        var totalYield = await useCase.GetTotalYieldByProductAndDateAsync(productPublicId, date);
        return Results.Ok(new { ProductPublicId = productPublicId, Date = date.ToString("yyyy-MM-dd"), TotalYield = totalYield });
    }

    private static async Task<IResult> StartBatchAsync(
        StartBatchRequest request,
        ProductionBatchUseCase useCase)
    {
        var batch = await useCase.StartBatchAsync(
            request.ProductPublicId,
            request.DailyMenuPublicId,
            request.ProductionDate,
            request.StartedByPublicId);

        return Results.Created($"/api/batches/{batch.PublicId}", ProductionBatchResponseMapper.ToResponse(batch));
    }

    private static async Task<IResult> CompleteBatchAsync(
        string publicId,
        CompleteBatchRequest request,
        ProductionBatchUseCase useCase)
    {
        var batch = await useCase.CompleteBatchAsync(
            publicId,
            request.Yield,
            request.CompletedByPublicId,
            request.Notes);

        return Results.Ok(ProductionBatchResponseMapper.ToResponse(batch));
    }
}







