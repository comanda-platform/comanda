namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class ProductMapper
{
    public static Product FromPersistence(this ProductDatabaseEntity dbEntity)
    {

        return Product.Rehydrate(
            dbEntity.PublicId,
            dbEntity.Name,
            dbEntity.Price,
            dbEntity.Type.FromPersistence(),
            dbEntity.Description,
            dbEntity.PriceHistory
                .OrderBy(p => p.EffectiveFrom)
                .Select(p => p.FromPersistence())
                .ToList()
        );
    }

    extension(Product domainEntity)
    {
        public ProductDatabaseEntity ToPersistence()
        {
            return new ProductDatabaseEntity
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                Description = domainEntity.Description,
                Price = domainEntity.CurrentPrice,
                Type = domainEntity.Type.ToPersistence(),

                PriceHistory = domainEntity.PriceHistory
                    .Select(p => p.ToPersistence())
                    .ToList()
            };
        }

        public void UpdatePersistence(ProductDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            dbEntity.Description = domainEntity.Description;
            dbEntity.Price = domainEntity.CurrentPrice;

            // Sync price history
            {
                var domainHistoryPublicIds = domainEntity.PriceHistory
                    .Select(p => p.PublicId)
                    .ToHashSet();

                var entitiesToRemove = dbEntity.PriceHistory
                    .Where(e => !domainHistoryPublicIds.Contains(e.PublicId))
                    .ToList();

                foreach (var toRemove in entitiesToRemove)
                {
                    dbEntity.PriceHistory.Remove(toRemove);
                }

                foreach (var historyEntry in domainEntity.PriceHistory)
                {
                    var existing = dbEntity.PriceHistory.FirstOrDefault(e => e.PublicId == historyEntry.PublicId);

                    if (existing != null)
                    {
                        historyEntry.UpdatePersistence(existing);
                    }
                    else
                    {
                        dbEntity.PriceHistory.Add(historyEntry.ToPersistence());
                    }
                }
            }
        }
    }
}






