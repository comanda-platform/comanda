namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class RecipeRepository(Context context) : GenericDatabaseRepository<RecipeDatabaseEntity>(context), IRecipeRepository
{
    private IQueryable<RecipeDatabaseEntity> QueryWithIncludes() =>
        Query()
            .Include(r => r.Product)
            .Include(r => r.Ingredients.Where(i => !i.IsDeleted))
                .ThenInclude(i => i.Item)
                    .ThenInclude(item => item.BaseUnit)
            .Include(r => r.Ingredients.Where(i => !i.IsDeleted))
                .ThenInclude(i => i.Unit);

    public override async Task<RecipeDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await QueryWithIncludes().FirstOrDefaultAsync(r => r.PublicId == publicId);

    public async Task<RecipeDatabaseEntity?> GetByProductIdAsync(int productId) =>
        await QueryWithIncludes().FirstOrDefaultAsync(r => r.ProductId == productId);

    public async Task<RecipeDatabaseEntity?> GetByProductPublicIdAsync(string productPublicId) =>
        await QueryWithIncludes().FirstOrDefaultAsync(r => r.Product.PublicId == productPublicId);
}







