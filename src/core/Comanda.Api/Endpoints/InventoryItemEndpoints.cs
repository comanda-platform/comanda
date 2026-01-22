namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class InventoryItemEndpoints
{
    public static void MapInventoryItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventory-items")
            .WithTags("Inventory Items")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get inventory items with optional filters")
            .WithDescription("Retrieves inventory items. Use query parameters to filter: searchTerm");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get inventory item by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new inventory item");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchInventoryItemAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update inventory item fields")
            .WithDescription("Update inventory item name or base unit");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete an inventory item");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] InventoryItemQueryParameters query,
        InventoryItemUseCase UseCase)
    {
        IEnumerable<Domain.Entities.InventoryItem> items;

        // Apply filters based on query parameters
        if (!string.IsNullOrEmpty(query.SearchTerm))
        {
            items = await UseCase.SearchByNameAsync(query.SearchTerm);
        }
        else
        {
            items = await UseCase.GetAllAsync();
        }

        return Results.Ok(items.Select(InventoryItemResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        InventoryItemUseCase UseCase)
    {
        var item = await UseCase.GetByPublicIdAsync(publicId);

        return Results.Ok(InventoryItemResponseMapper.ToResponse(item));
    }

    private static async Task<IResult> CreateAsync(
        CreateInventoryItemRequest request,
        InventoryItemUseCase UseCase)
    {
        var item = await UseCase.CreateAsync(
            request.Name,
            request.BaseUnitPublicId);

        return Results.Created(
            $"/api/inventory-items/{item.PublicId}",
            InventoryItemResponseMapper.ToResponse(item));
    }

    private static async Task<IResult> PatchInventoryItemAsync(
        string publicId,
        PatchInventoryItemRequest request,
        InventoryItemUseCase UseCase)
    {
        // Update name if provided
        if (!string.IsNullOrEmpty(request.Name))
        {
            await UseCase.UpdateNameAsync(publicId, request.Name);
        }

        // Update base unit if provided
        if (!string.IsNullOrEmpty(request.BaseUnitPublicId))
        {
            await UseCase.UpdateBaseUnitAsync(publicId, request.BaseUnitPublicId);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        InventoryItemUseCase UseCase)
    {
        await UseCase.DeleteAsync(publicId);

        return Results.NoContent();
    }
}







