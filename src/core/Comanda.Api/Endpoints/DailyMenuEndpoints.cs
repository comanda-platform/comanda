namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class DailyMenuEndpoints
{
    public static void MapDailyMenuEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/daily-menus")
            .WithTags("Daily Menus")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get daily menus with optional filters")
            .WithDescription("Retrieves daily menus. Use query parameters to filter: date, from/to, locationId");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get daily menu by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new daily menu");

        group.MapPost("/{menuPublicId}/items", AddItemAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Add an item to a daily menu");

        group.MapPost("/{menuPublicId}/reorder", ReorderItemsAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Reorder menu items");
        #endregion

        #region PUT
        group.MapPut("/{menuPublicId}/items/{menuItemPublicId}", UpdateItemAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update a menu item");
        #endregion

        #region DELETE
        group.MapDelete("/{menuPublicId}/items/{menuItemPublicId}", RemoveItemAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Remove an item from a daily menu");

        group.MapDelete("/{menuPublicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a daily menu");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] DailyMenuQueryParameters query,
        DailyMenuUseCase UseCase)
    {
        IEnumerable<Domain.Entities.DailyMenu> menus;

        // Apply filters based on query parameters
        if (query.Date.HasValue)
        {
            var menu = await UseCase.GetByDateAsync(query.Date.Value, query.LocationId);
            if (menu == null)
                return Results.NotFound();
            return Results.Ok(DailyMenuResponseMapper.ToResponse(menu));
        }
        else if (query.From.HasValue && query.To.HasValue)
        {
            menus = await UseCase.GetByDateRangeAsync(query.From.Value, query.To.Value, query.LocationId);
        }
        else if (!string.IsNullOrEmpty(query.LocationId))
        {
            menus = await UseCase.GetByLocationAsync(query.LocationId);
        }
        else
        {
            menus = await UseCase.GetAllAsync();
        }

        return Results.Ok(menus.Select(DailyMenuResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        DailyMenuUseCase UseCase)
    {
        var menu = await UseCase.GetByPublicIdAsync(publicId);

        return Results.Ok(DailyMenuResponseMapper.ToResponse(menu));
    }

    private static async Task<IResult> CreateAsync(CreateDailyMenuRequest request, DailyMenuUseCase UseCase)
    {
        var menu = await UseCase.CreateAsync(request.Date, request.LocationPublicId);
        return Results.Created($"/api/daily-menus/{menu.PublicId}", DailyMenuResponseMapper.ToResponse(menu));
    }

    private static async Task<IResult> AddItemAsync(
        string menuPublicId,
        AddDailyMenuItemRequest request,
        DailyMenuUseCase UseCase)
    {
        await UseCase.AddMenuItemAsync(
            menuPublicId,
            request.ProductPublicId,
            request.SequenceOrder,
            request.OverriddenName,
            request.OverriddenPrice);

        return Results.NoContent();
    }

    private static async Task<IResult> UpdateItemAsync(
        string menuPublicId,
        string menuItemPublicId,
        UpdateDailyMenuItemRequest request,
        DailyMenuUseCase UseCase)
    {
        await UseCase.UpdateMenuItemAsync(
            menuPublicId,
            menuItemPublicId,
            request.SequenceOrder,
            request.OverriddenName,
            request.OverriddenPrice);

        return Results.NoContent();
    }

    private static async Task<IResult> RemoveItemAsync(string menuPublicId, string menuItemPublicId, DailyMenuUseCase UseCase)
    {
        await UseCase.RemoveMenuItemAsync(menuPublicId, menuItemPublicId);
        return Results.NoContent();
    }

    private static async Task<IResult> ReorderItemsAsync(string menuPublicId, DailyMenuUseCase UseCase)
    {
        await UseCase.ReorderMenuItemsAsync(menuPublicId);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(string menuPublicId, DailyMenuUseCase UseCase)
    {
        await UseCase.DeleteAsync(menuPublicId);
        return Results.NoContent();
    }
}







