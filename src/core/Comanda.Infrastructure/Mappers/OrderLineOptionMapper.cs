namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class OrderLineOptionMapper
{
    extension(OrderLineOptionDatabaseEntity dbEntity)
    {
        public OrderLineOption FromPersistence() => 
            OrderLineOption.Rehydrate(
                dbEntity.PublicId,
                dbEntity.OptionKey,
                dbEntity.IsIncluded,
                dbEntity.CreatedAt,
                dbEntity.OrderLineId);
    }

    extension(OrderLineOption domainEntity)
    {
        public OrderLineOptionDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                OptionKey = domainEntity.OptionKey,
                IsIncluded = domainEntity.IsIncluded,
                CreatedAt = domainEntity.CreatedAt,
                OrderLineId = domainEntity.OrderLineId
            };

        public void UpdatePersistence(OrderLineOptionDatabaseEntity dbEntity)
        {
            dbEntity.OptionKey = domainEntity.OptionKey;
            dbEntity.IsIncluded = domainEntity.IsIncluded;
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







