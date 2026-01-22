namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using System.Text.Json;

public static class OrderLineMapper
{
    extension(OrderLineDatabaseEntity dbEntity)
    {
        public OrderLine FromPersistence() 
        {
            List<string>? selectedSides = null;
            if (!string.IsNullOrEmpty(dbEntity.SelectedSidesJson))
            {
                selectedSides = JsonSerializer.Deserialize<List<string>>(dbEntity.SelectedSidesJson);
            }
            
            return OrderLine.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Order.PublicId,
                dbEntity.Product.PublicId,
                dbEntity.Quantity,
                dbEntity.UnitPrice,
                dbEntity.Client?.PublicId,
                dbEntity.PrepStatus,
                dbEntity.PrepStartedAt,
                dbEntity.PrepCompletedAt,
                dbEntity.ContainerType,
                selectedSides
            );
        }
    }

    extension(OrderLine domainEntity)
    {
        public OrderLineDatabaseEntity ToPersistence(
        OrderDatabaseEntity orderDbEntity,
        ProductDatabaseEntity productDbEntity) => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Order = orderDbEntity, 
                Product = productDbEntity,
                Quantity = domainEntity.Quantity,
                UnitPrice = domainEntity.UnitPrice,
                PrepStatus = domainEntity.PrepStatus,
                PrepStartedAt = domainEntity.PrepStartedAt,
                PrepCompletedAt = domainEntity.PrepCompletedAt,
                ContainerType = domainEntity.ContainerType,
                SelectedSidesJson = domainEntity.SelectedSides != null 
                    ? JsonSerializer.Serialize(domainEntity.SelectedSides) 
                    : null
            };

        public void UpdatePersistence(OrderLineDatabaseEntity dbEntity)
        {
            dbEntity.Quantity = domainEntity.Quantity;
            dbEntity.PrepStatus = domainEntity.PrepStatus;
            dbEntity.PrepStartedAt = domainEntity.PrepStartedAt;
            dbEntity.PrepCompletedAt = domainEntity.PrepCompletedAt;
            dbEntity.ContainerType = domainEntity.ContainerType;
            dbEntity.SelectedSidesJson = domainEntity.SelectedSides != null 
                ? JsonSerializer.Serialize(domainEntity.SelectedSides) 
                : null;
        }
    }
}







