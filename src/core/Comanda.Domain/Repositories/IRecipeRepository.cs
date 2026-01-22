namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface IRecipeRepository
{
    Task<Recipe?> GetByIdAsync(int id);
    Task<Recipe?> GetByPublicIdAsync(string publicId);
    Task<Recipe?> GetByProductIdAsync(int productId);
    Task<Recipe?> GetByProductPublicIdAsync(string productPublicId);
    Task<IEnumerable<Recipe>> GetAllAsync();
    Task AddAsync(Recipe recipe);
    Task UpdateAsync(Recipe recipe);
    Task DeleteAsync(Recipe recipe);
}







