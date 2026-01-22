namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class OrderMapper
{
    extension(OrderDatabaseEntity entity)
    {
        public Order FromPersistence()
        {
            return Order.Rehydrate(
                entity.PublicId,
                entity.CreatedAt,
                (OrderStatus)entity.OrderStatusTypeId,
                (OrderFulfillmentType)entity.FulfillmentTypeId,
                (OrderSource)entity.OrderSourceId,
                entity.Client?.PublicId,
                entity.ClientGroup?.PublicId,
                entity.Location?.PublicId,

                lines: entity.Lines?
                    .Select(l => l.FromPersistence())
                    .ToList() ?? [],

                statusHistory: [] // TODO Status history needs to be loaded separately or eagerly
            );
        }

        public Order FromPersistence(
            IEnumerable<OrderStatusHistoryDatabaseEntity> statusHistory)
        {
            return Order.Rehydrate(
                entity.PublicId,
                entity.CreatedAt,
                (OrderStatus)entity.OrderStatusTypeId,
                (OrderFulfillmentType)entity.FulfillmentTypeId,
                (OrderSource)entity.OrderSourceId,
                entity.Client?.PublicId,
                entity.ClientGroup?.PublicId,
                entity.Location?.PublicId,

                lines: entity.Lines?
                    .Select(l => l.FromPersistence())
                    .ToList() ?? new List<OrderLine>(),

                statusHistory: statusHistory
                    .Select(h => h.FromPersistence())
                    .ToList()
            );
        }
    }

    // TODO: To be set in infrastructure layer
    public static OrderDatabaseEntity ToPersistence(this Order order)
    {

        // Lines are NOT included here because they require Product navigation property
        // Lines should be added separately in the repository adapter
        return new OrderDatabaseEntity
        {
            //Id = domain.Id,
            PublicId = order.PublicId,
            CreatedAt = order.CreatedAt,
            OrderStatusTypeId = (int)order.Status,
            FulfillmentTypeId = (int)order.FulfillmentType,
            OrderSourceId = (int)order.Source,
            //ClientId = domain.ClientId,
            //ClientGroupId = domain.ClientGroupId,
            //LocationId = domain.LocationId
        };
    }

    public static void UpdatePersistence(
        this Order order,
        OrderDatabaseEntity entity)
    {
        entity.OrderStatusTypeId = (int)order.Status;
        //entity.LocationId = domain.LocationId; // TODO: To be set in infrastructure layer
        entity.LastModifiedAt = DateTime.UtcNow;

        // Line syncing is handled in the repository adapter
        // because new lines need Product navigation property
    }
}







