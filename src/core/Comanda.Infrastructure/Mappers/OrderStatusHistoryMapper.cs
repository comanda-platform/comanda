namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class OrderStatusHistoryMapper
{
    extension(OrderStatusHistoryDatabaseEntity dbEntity)
    {
        public OrderStatusHistory FromPersistence() => 
            OrderStatusHistory.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Order.PublicId,
                (OrderStatus)dbEntity.OrderStatusTypeId,
                dbEntity.ChangedAt,
                dbEntity.ChangedBy?.PublicId
            );
    }

    // TODO: To be set in infrastructure layer
    public static OrderStatusHistoryDatabaseEntity ToPersistence(
        this OrderStatusHistory orderStatusHistoryDomainEntity,
        OrderDatabaseEntity orderDbEntity) =>
        new()
        {
            //Id = domain.Id,
            PublicId = orderStatusHistoryDomainEntity.PublicId,
            OrderId = orderDbEntity.Id,
            Order = orderDbEntity,
            OrderStatusTypeId = (int)orderStatusHistoryDomainEntity.Status,
            ChangedAt = orderStatusHistoryDomainEntity.ChangedAt,
            //ChangedById = domain.ChangedById
        };
}







