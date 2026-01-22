namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class PriceHistoryMapper
{
    extension(ProductPriceHistoryDatabaseEntity dbEntity)
    {
        public PriceHistoryEntry FromPersistence() => 
            PriceHistoryEntry.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Price,
                dbEntity.EffectiveFrom,
                dbEntity.EffectiveTo
            );
    }

    extension(PriceHistoryEntry domainEntity)
    {
        //TODO: To be set in infrastructure layer
        public ProductPriceHistoryDatabaseEntity ToPersistence() => new()
        {
            //Id = domain.Id,
            Price = domainEntity.Price,
            EffectiveFrom = domainEntity.EffectiveFrom,
            EffectiveTo = domainEntity.EffectiveTo,
            Product = null! // To be set by the caller
        };

        public void UpdatePersistence(ProductPriceHistoryDatabaseEntity dbEntity)
        {
            dbEntity.Price = domainEntity.Price;
            dbEntity.EffectiveFrom = domainEntity.EffectiveFrom;
            dbEntity.EffectiveTo = domainEntity.EffectiveTo;
        }
    }
}






