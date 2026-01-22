namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class RecipeRepositoryAdapter(
    Database.Repositories.IRecipeRepository databaseRepository,
    Context context) : Domain.Repositories.IRecipeRepository
{
    private readonly Database.Repositories.IRecipeRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Recipe?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Recipe?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<Recipe?> GetByProductIdAsync(int productId)
    {
        var entity = await _databaseRepository.GetByProductIdAsync(productId);

        return entity?.FromPersistence();
    }

    public async Task<Recipe?> GetByProductPublicIdAsync(string productPublicId)
    {
        var entity = await _databaseRepository.GetByProductPublicIdAsync(productPublicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Recipe>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Recipe recipe)
    {
        var entity = recipe.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Recipe recipe)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(recipe.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Recipe, recipe.PublicId);

        recipe.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Recipe recipe)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(recipe.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Recipe, recipe.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}







