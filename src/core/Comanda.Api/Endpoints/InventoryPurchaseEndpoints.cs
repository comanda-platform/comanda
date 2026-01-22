namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Comanda.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

public static class InventoryPurchaseEndpoints
{
    public static void MapInventoryPurchaseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventory-purchases")
            .WithTags("Inventory Purchases")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get inventory purchases with optional filters")
            .WithDescription("Retrieves inventory purchases. Use query parameters to filter: pendingDelivery, type, from/to, supplierId");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get inventory purchase by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new inventory purchase");

        group.MapPost("/{publicId}/lines", AddLineAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Add a line to an inventory purchase");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchInventoryPurchaseAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update inventory purchase fields")
            .WithDescription("Update purchase type or mark as delivered");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete an inventory purchase");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] InventoryPurchaseQueryParameters query,
        InventoryPurchaseUseCase UseCase)
    {
        IEnumerable<Domain.Entities.InventoryPurchase> purchases;

        // Apply filters based on query parameters
        if (query.PendingDelivery.HasValue && query.PendingDelivery.Value)
        {
            purchases = await UseCase.GetPendingDeliveryAsync();
        }
        else if (query.Type.HasValue)
        {
            purchases = await UseCase.GetByTypeAsync(query.Type.Value);
        }
        else if (query.From.HasValue && query.To.HasValue)
        {
            purchases = await UseCase.GetByDateRangeAsync(query.From.Value, query.To.Value);
        }
        else if (!string.IsNullOrEmpty(query.SupplierId))
        {
            purchases = await UseCase.GetBySupplierAsync(query.SupplierId);
        }
        else
        {
            purchases = await UseCase.GetAllAsync();
        }

        return Results.Ok(purchases.Select(InventoryPurchaseResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        InventoryPurchaseUseCase UseCase)
    {
        var purchase = await UseCase.GetByPublicIdAsync(publicId);

        return Results.Ok(InventoryPurchaseResponseMapper.ToResponse(purchase));
    }

    private static async Task<IResult> CreateAsync(
        CreateInventoryPurchaseRequest request,
        InventoryPurchaseUseCase UseCase)
    {
        var purchase = await UseCase.CreateAsync(
            request.SupplierPublicId,
            request.PurchaseType,
            request.PurchasedAt,
            request.StoreLocationPublicId);

        return Results.Created(
            $"/api/inventory-purchases/{purchase.PublicId}",
            InventoryPurchaseResponseMapper.ToResponse(purchase));
    }

    private static async Task<IResult> AddLineAsync(
        string publicId,
        AddInventoryPurchaseLineRequest request,
        InventoryPurchaseUseCase UseCase)
    {
        await UseCase.AddLineAsync(
            publicId,
            request.InventoryItemPublicId,
            request.Quantity,
            request.UnitPrice,
            request.UnitPublicId);

        return Results.NoContent();
    }

    private static async Task<IResult> PatchInventoryPurchaseAsync(
        string publicId,
        PatchInventoryPurchaseRequest request,
        InventoryPurchaseUseCase UseCase)
    {
        // Update type if provided
        if (request.PurchaseType.HasValue)
        {
            await UseCase.UpdateTypeAsync(publicId, request.PurchaseType.Value);
        }

        // Mark as delivered if provided
        if (request.DeliveredAt.HasValue)
        {
            await UseCase.MarkAsDeliveredAsync(publicId, request.DeliveredAt);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        InventoryPurchaseUseCase UseCase)
    {
        await UseCase.DeleteAsync(publicId);

        return Results.NoContent();
    }
}







