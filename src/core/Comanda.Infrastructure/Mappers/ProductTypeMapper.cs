namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class ProductTypeMapper
{
    extension(ProductTypeDatabaseEntity dbEntity)
    {
        public ProductType FromPersistence() => ProductType.Rehydrate(
            dbEntity.PublicId,
            dbEntity.Name);
    }

    extension(ProductType domainEntity)
    {
        public ProductTypeDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name
            };
    }
}






