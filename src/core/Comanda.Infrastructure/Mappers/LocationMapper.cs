namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class LocationMapper
{
    extension(LocationDatabaseEntity dbEntity)
    {
        public Location FromPersistence() =>
            Location.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Name,
                dbEntity.Latitude,
                dbEntity.Longitude,
                dbEntity.AddressLine,
                dbEntity.IsActive,
                (LocationType)dbEntity.LocationTypeId,
                dbEntity.Client?.PublicId,
                dbEntity.ClientGroup?.PublicId,
                dbEntity.CreatedAt
            );
    }

    extension(Location domainEntity)
    {
        public LocationDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                Latitude = domainEntity.Latitude,
                Longitude = domainEntity.Longitude,
                AddressLine = domainEntity.AddressLine,
                IsActive = domainEntity.IsActive,
                LocationTypeId = (int)domainEntity.Type,
                //ClientId = domain.ClientId, // TODO: To be set on the object in infrastructure layer 
                //ClientGroupId = domain.ClientGroupId,  // TODO: To be set on the object in infrastructure layer 
                CreatedAt = DateTime.UtcNow
            };

        public void UpdatePersistence(LocationDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            dbEntity.Latitude = domainEntity.Latitude;
            dbEntity.Longitude = domainEntity.Longitude;
            dbEntity.AddressLine = domainEntity.AddressLine;
            dbEntity.IsActive = domainEntity.IsActive;
            dbEntity.LocationTypeId = (int)domainEntity.Type;
            //entity.ClientId = domain.ClientId; // TODO: To be set on the object in infrastructure layer 
            //entity.ClientGroupId = domain.ClientGroupId; // TODO: To be set on the object in infrastructure layer 
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}






