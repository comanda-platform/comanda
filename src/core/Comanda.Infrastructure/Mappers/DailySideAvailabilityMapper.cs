namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class DailySideAvailabilityMapper
{
    extension(DailySideAvailabilityDatabaseEntity entity)
    {
        public DailySideAvailability FromPersistence() => 
            DailySideAvailability.Rehydrate(
                entity.PublicId,
                entity.Date,
                entity.IsAvailable,
                entity.CreatedAt,
                entity.Side.FromPersistence());
    }

    extension(DailySideAvailability domain)
    {
        public DailySideAvailabilityDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domain.PublicId,
                Date = domain.Date,
                IsAvailable = domain.IsAvailable,
                CreatedAt = domain.CreatedAt,
                //SideId = domain.Side.Id // TODO: To be set on the object in infrastructure layer 
            };

        public void UpdatePersistence(DailySideAvailabilityDatabaseEntity entity)
        {
            entity.IsAvailable = domain.IsAvailable;
            entity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







