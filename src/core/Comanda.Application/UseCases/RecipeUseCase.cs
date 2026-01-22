namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class RecipeUseCase(
    IRecipeRepository recipeRepository,
    IProductRepository productRepository,
    IInventoryItemRepository inventoryItemRepository,
    IUnitRepository unitRepository) : UseCaseBase(EntityTypePrintNames.Recipe)
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IInventoryItemRepository _inventoryItemRepository = inventoryItemRepository;
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<Recipe> CreateAsync(
        string name,
        string productPublicId)
    {
        var product = await _productRepository.GetByPublicIdAsync(productPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Product, productPublicId);

        var recipe = new Recipe(name, product);
        await _recipeRepository.AddAsync(recipe);
        return recipe;
    }

    public async Task<Recipe> GetByPublicIdAsync(string publicId)
        => await _recipeRepository.GetByPublicIdAsync(publicId)
        ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<Recipe> GetByProductPublicIdAsync(string productPublicId)
    {
        var product = await _productRepository.GetByPublicIdAsync(productPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Product, productPublicId);

        return await _recipeRepository.GetByProductPublicIdAsync(product.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Product, productPublicId);
    }

    public async Task<IEnumerable<Recipe>> GetAllAsync()
        => await _recipeRepository.GetAllAsync();

    public async Task UpdateNameAsync(string publicId, string name)
    {
        var recipe = await _recipeRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        recipe.UpdateName(name);

        await _recipeRepository.UpdateAsync(recipe);
    }

    public async Task AddIngredientAsync(
        string recipePublicId,
        string inventoryItemPublicId,
        string unitPublicId,
        decimal quantity)
    {
        var recipe = await _recipeRepository.GetByPublicIdAsync(recipePublicId)
            ?? throw new NotFoundException(EntityTypePrintName, recipePublicId);

        var item = await _inventoryItemRepository.GetByPublicIdAsync(inventoryItemPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.InventoryItem, inventoryItemPublicId);

        var unit = await _unitRepository.GetByPublicIdAsync(unitPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Unit, unitPublicId);

        var ingredient = new RecipeIngredient(item, quantity, unit);

        recipe.AddIngredient(ingredient);

        await _recipeRepository.UpdateAsync(recipe);
    }

    public async Task UpdateIngredientQuantityAsync(
        string recipePublicId,
        string ingredientPublicId,
        decimal quantity)
    {
        var recipe = await _recipeRepository.GetByPublicIdAsync(recipePublicId)
            ?? throw new NotFoundException(EntityTypePrintName, recipePublicId);

        var ingredient = recipe.Ingredients.FirstOrDefault(i => i.PublicId == ingredientPublicId)
            ?? throw new NotFoundException($"Ingredient with ID '{ingredientPublicId}' not found in recipe");

        ingredient.UpdateQuantity(quantity);

        await _recipeRepository.UpdateAsync(recipe);
    }

    public async Task RemoveIngredientAsync(
        string recipePublicId,
        string ingredientPublicId)
    {
        var recipe = await _recipeRepository.GetByPublicIdAsync(recipePublicId)
            ?? throw new NotFoundException(EntityTypePrintName, recipePublicId);

        var ingredient = recipe.Ingredients.FirstOrDefault(i => i.PublicId == ingredientPublicId)
            ?? throw new NotFoundException($"Ingredient with ID '{ingredientPublicId}' not found in recipe");

        recipe.RemoveIngredient(ingredient);
        await _recipeRepository.UpdateAsync(recipe);
    }

    public async Task DeleteAsync(string publicId)
    {
        var recipe = await _recipeRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _recipeRepository.DeleteAsync(recipe);
    }
}







