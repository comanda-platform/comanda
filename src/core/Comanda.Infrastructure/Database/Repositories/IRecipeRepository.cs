namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IRecipeRepository : IGenericDatabaseRepository<RecipeDatabaseEntity>
{
    Task<RecipeDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<RecipeDatabaseEntity?> GetByProductIdAsync(int productId);
    Task<RecipeDatabaseEntity?> GetByProductPublicIdAsync(string productPublicId);
}







