using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class OrderResponseMapper
{
    public static OrderResponse ToResponse(Domain.Entities.Order order)
    {
        return new OrderResponse(
            order.PublicId,
            order.ClientPublicId,
            order.ClientGroupPublicId,
            order.LocationPublicId,
            order.Source,
            order.FulfillmentType,
            order.Status,
            order.TotalAmount,
            order.TotalItems,
            order.Lines?.Select(l => new OrderLineResponse(
                l.PublicId,
                l.ProductPublicId,
                l.Quantity,
                l.UnitPrice,
                l.LineTotal,
                l.PrepStatus,
                l.PrepStartedAt,
                l.PrepCompletedAt,
                l.ContainerType,
                l.SelectedSides)) ?? Enumerable.Empty<OrderLineResponse>(),
            order.CreatedAt);
    }
}







