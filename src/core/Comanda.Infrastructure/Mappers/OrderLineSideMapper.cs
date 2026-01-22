namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class OrderLineSideMapper
{
    extension(OrderLineSideDatabaseEntity dbEntity)
    {
        public OrderLineSide FromPersistence() => 
            OrderLineSide.Rehydrate(
                dbEntity.OrderLine.PublicId,

                dbEntity.Side.FromPersistence()
            );
    }

    public static OrderLineSideDatabaseEntity ToPersistence(this OrderLineSide domainEntity) => 
        new()
        {
            //OrderLinePublicId = domain.OrderLineId, // TODO: To be set in infrastructure layer
        //SideId = domain.SideId // TODO: To be set in infrastructure layer
        };
}







