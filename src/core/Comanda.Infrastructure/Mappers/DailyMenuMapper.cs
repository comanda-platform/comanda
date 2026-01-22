namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class DailyMenuMapper
{
    extension(DailyMenuDatabaseEntity dbEntity)
    {
        public DailyMenu FromPersistence() => 
            DailyMenu.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Date,
                dbEntity.CreatedAt,
                dbEntity.Location.PublicId,

                items: dbEntity.Items
                    .Where(i => !i.IsDeleted)
                    .OrderBy(i => i.SequenceOrder)
                    .Select(i => i.FromPersistence())
                    .ToList());
    }

    extension(DailyMenu domainEntity)
    {
        public DailyMenuDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Date = domainEntity.Date,
                CreatedAt = domainEntity.CreatedAt,
                //LocationId = domain.LocationId // TODO: To be set in infrastructure layer
            };

        public void UpdatePersistence(DailyMenuDatabaseEntity dbEntity)
        {
            //entity.LocationId = domain.LocationId; // TODO: To be set in infrastructure layer
            dbEntity.LastModifiedAt = DateTime.UtcNow;

            // Sync items
            var domainItemPublicIds = domainEntity.Items
                .Select(i => i.PublicId)
                .ToHashSet();

            var itemsToRemove = dbEntity.Items
                .Where(e => !domainItemPublicIds.Contains(e.PublicId))
                .ToList();

            foreach (var toRemove in itemsToRemove)
            {
                dbEntity.Items.Remove(toRemove);
            }

            foreach (var item in domainEntity.Items)
            {
                var existing = dbEntity.Items.FirstOrDefault(e => e.PublicId == item.PublicId);

                if (existing != null)
                {
                    item.UpdatePersistence(existing);
                }
            }
        }
    }
}







