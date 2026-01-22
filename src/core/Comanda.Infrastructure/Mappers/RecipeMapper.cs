namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class RecipeMapper
{
    extension(RecipeDatabaseEntity dbEntity)
    {
        public Recipe FromPersistence() => 
            Recipe.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Name,
                dbEntity.CreatedAt,

                dbEntity.Product.FromPersistence(),
                dbEntity.EstimatedPortions,

                dbEntity.Ingredients
                    .Where(i => !i.IsDeleted)
                    .OrderBy(i => i.Id)
                    .Select(i => i.FromPersistence())
                    .ToList());
    }

    extension(Recipe domainEntity)
    {
        // TODO: To be set in infrastructure layer
        public RecipeDatabaseEntity ToPersistence()
        {
            return new RecipeDatabaseEntity
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                CreatedAt = domainEntity.CreatedAt,
                EstimatedPortions = domainEntity.EstimatedPortions,
                //ProductId = domain.Product.Id,
                Ingredients = domainEntity.Ingredients.Select(i => i.ToPersistence()).ToList()
            };
        }

        public void UpdatePersistence(RecipeDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            dbEntity.EstimatedPortions = domainEntity.EstimatedPortions;
            //dbEntity.ProductId = domainEntity.Product.Id; // TODO: To be set in infrastructure layer
            dbEntity.LastModifiedAt = DateTime.UtcNow;

            // Sync ingredients
            var domainIngredientPublicIds = domainEntity.Ingredients
                .Select(i => i.PublicId)
                .ToHashSet();

            var ingredientsToRemove = dbEntity.Ingredients
                .Where(e => !domainIngredientPublicIds.Contains(e.PublicId))
                .ToList();

            foreach (var toRemove in ingredientsToRemove)
            {
                dbEntity.Ingredients.Remove(toRemove);
            }

            foreach (var ingredient in domainEntity.Ingredients)
            {
                var existing = dbEntity.Ingredients.FirstOrDefault(e => e.PublicId == ingredient.PublicId);

                if (existing != null)
                {
                    ingredient.UpdatePersistence(existing);
                }
                else
                {
                    dbEntity.Ingredients.Add(ingredient.ToPersistence());
                }
            }
        }
    }
}







