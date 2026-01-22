namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class RecipeIngredientMapper
{
    extension(RecipeIngredientDatabaseEntity dbEntity)
    {
        public RecipeIngredient FromPersistence() 
            => RecipeIngredient.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Quantity,
                dbEntity.CreatedAt,
                dbEntity.Item.FromPersistence(),
                dbEntity.Unit.FromPersistence(),
                dbEntity.Recipe.PublicId
            );
    }

    extension(RecipeIngredient domainEntity)
    {
        //TODO: To be set in infrastructure layer
        public RecipeIngredientDatabaseEntity ToPersistence() => new()
        {
            PublicId = domainEntity.PublicId,
            Quantity = domainEntity.Quantity,
            CreatedAt = domainEntity.CreatedAt,
            //RecipeId = domain.RecipeId,
            //InventoryItemId = domain.Item.Id,
            //UnitId = domain.Unit.Id
        };

        public void UpdatePersistence(RecipeIngredientDatabaseEntity dbEntity)
        {
            dbEntity.Quantity = domainEntity.Quantity;
            //entity.InventoryItemId = domain.Item.Id;
            //entity.UnitId = domain.Unit.Id;
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







