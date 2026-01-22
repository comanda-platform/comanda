namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class DailyMenuItemMapper
{
    extension(DailyMenuItemDatabaseEntity dbEntity)
    {
        public DailyMenuItem FromPersistence() =>
            DailyMenuItem.Rehydrate(
                dbEntity.PublicId,
                dbEntity.SequenceOrder,
                dbEntity.OverriddenName,
                dbEntity.OverriddenPrice,
                dbEntity.CreatedAt,
                dbEntity.Product.FromPersistence(),
                dbEntity.DailyMenuId);
    }

    extension(DailyMenuItem domainEntity)
    {
        public DailyMenuItemDatabaseEntity ToPersistence(
            DailyMenuDatabaseEntity dailyMenuDbEntity,
            ProductDatabaseEntity productDbEntity) => 
            new()
            {
                SequenceOrder = domainEntity.SequenceOrder,
                OverriddenName = domainEntity.OverriddenName,
                OverriddenPrice = domainEntity.OverriddenPrice,
                CreatedAt = domainEntity.CreatedAt,
                Menu = dailyMenuDbEntity,
                Product = productDbEntity
                //ProductId = domain.Product.Id, // TODO: To be set in infrastructure layer
                //DailyMenuId = domain.DailyMenuId, // TODO: To be set in infrastructure layer
            };

        public void UpdatePersistence(DailyMenuItemDatabaseEntity dailyMenuItemDbEntity)
        {
            dailyMenuItemDbEntity.SequenceOrder = domainEntity.SequenceOrder;
            dailyMenuItemDbEntity.OverriddenName = domainEntity.OverriddenName;
            dailyMenuItemDbEntity.OverriddenPrice = domainEntity.OverriddenPrice;
            //entity.ProductId = domain.Product.Id; // TODO: To be set in infrastructure layer
            dailyMenuItemDbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







