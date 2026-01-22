namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Comanda.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders")
            .WithTags("Orders")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetOrdersAsync)
            .WithSummary("Get orders with optional filters")
            .WithDescription("Retrieves orders. Use query parameters to filter: active, clientId, clientGroupId, status, from, to");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get order by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new order");

        group.MapPost("/{publicId}/lines", AddLineAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Add a line to an order");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchOrderAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update order status or fields")
            .WithDescription("Update order status (accepted, preparing, ready, in_transit, delivered, completed, cancelled) or delivery location");

        group.MapPatch("/lines/{linePublicId}", PatchOrderLineAsync)
            .AddEndpointFilter(new RequireNonEmptyStringFilter(0, "linePublicId"))
            .WithSummary("Update order line status")
            .WithDescription("Update order line status (preparing, plated)");
        #endregion
    }

    private static async Task<IResult> GetOrdersAsync(
        [AsParameters] OrderQueryParameters query,
        OrderUseCase useCase)
    {
        IEnumerable<Domain.Entities.Order> orders;

        // Apply filters based on query parameters
        if (query.Active.HasValue && query.Active.Value)
        {
            orders = await useCase.GetActiveOrdersAsync();
        }
        else if (!string.IsNullOrEmpty(query.ClientId))
        {
            orders = await useCase.GetOrdersByClientAsync(query.ClientId);
        }
        else if (!string.IsNullOrEmpty(query.ClientGroupId))
        {
            orders = await useCase.GetOrdersByClientGroupAsync(query.ClientGroupId);
        }
        else if (query.Status.HasValue)
        {
            orders = await useCase.GetOrdersByStatusAsync(query.Status.Value);
        }
        else if (query.From.HasValue && query.To.HasValue)
        {
            orders = await useCase.GetOrdersByDateRangeAsync(query.From.Value, query.To.Value);
        }
        else
        {
            orders = await useCase.GetAllOrdersAsync();
        }

        return Results.Ok(orders.Select(OrderResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        OrderUseCase useCase)
    {
        var order = await useCase.GetOrderByPublicIdAsync(publicId);
        return Results.Ok(OrderResponseMapper.ToResponse(order));
    }

    private static async Task<IResult> CreateAsync(
        CreateOrderRequest request,
        OrderUseCase useCase)
    {
        var order = await useCase.CreateOrderAsync(
            request.FulfillmentType,
            request.Source,
            request.ClientPublicId,
            request.ClientGroupPublicId,
            request.LocationPublicId);

        return Results.Created(
            $"/api/orders/{order.PublicId}",
            OrderResponseMapper.ToResponse(order));
    }

    private static async Task<IResult> AddLineAsync(
        string publicId,
        AddOrderLineRequest request,
        OrderUseCase useCase)
    {
        await useCase.AddOrderLineAsync(
            publicId,
            request.ProductPublicId,
            request.Quantity,
            request.ClientPublicId,
            request.SelectedSides);

        return Results.NoContent();
    }

    private static async Task<IResult> PatchOrderAsync(
        string publicId,
        PatchOrderRequest request,
        OrderUseCase useCase)
    {
        // Update delivery location if provided
        if (!string.IsNullOrEmpty(request.DeliveryLocationPublicId))
        {
            await useCase.AssignDeliveryLocationAsync(publicId, request.DeliveryLocationPublicId);
        }

        // Update status if provided
        if (request.Status.HasValue)
        {
            switch (request.Status.Value)
            {
                case OrderStatus.Accepted:
                    await useCase.AcceptOrderAsync(publicId, request.ChangedByPublicId);
                    break;
                case OrderStatus.Preparing:
                    await useCase.StartPreparingOrderAsync(publicId, request.ChangedByPublicId);
                    break;
                case OrderStatus.Ready:
                    await useCase.MarkOrderReadyAsync(publicId, request.ChangedByPublicId);
                    break;
                case OrderStatus.InTransit:
                    await useCase.StartDeliveryAsync(publicId, request.ChangedByPublicId);
                    break;
                case OrderStatus.Delivered:
                    await useCase.MarkOrderDeliveredAsync(publicId, request.ChangedByPublicId);
                    break;
                case OrderStatus.Completed:
                    await useCase.CompleteOrderAsync(publicId, request.ChangedByPublicId);
                    break;
                case OrderStatus.Cancelled:
                    await useCase.CancelOrderAsync(publicId, request.ChangedByPublicId);
                    break;
                default:
                    return Results.BadRequest($"Invalid status: {request.Status.Value}");
            }
        }

        return Results.NoContent();
    }

    private static async Task<IResult> PatchOrderLineAsync(
        string linePublicId,
        PatchOrderLineRequest request,
        OrderUseCase useCase)
    {
        if (!request.Status.HasValue)
        {
            return Results.BadRequest("Status is required");
        }

        switch (request.Status.Value)
        {
            case OrderLineStatus.Preparing:
                await useCase.StartLinePrepAsync(linePublicId);
                break;
            case OrderLineStatus.Plated:
                await useCase.CompleteLinePlatingAsync(linePublicId);
                break;
            default:
                return Results.BadRequest($"Invalid status: {request.Status.Value}");
        }

        return Results.NoContent();
    }
}







