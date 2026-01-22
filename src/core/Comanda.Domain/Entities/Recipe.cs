namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class Recipe
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Product Product { get; private set; }
    public int? EstimatedPortions { get; private set; }

    private readonly List<RecipeIngredient> _ingredients = new();
    public IReadOnlyCollection<RecipeIngredient> Ingredients => _ingredients.AsReadOnly();

    private Recipe() { } // For reflection / serializers

    private Recipe(
        string publicId,
        string name,
        DateTime createdAt,
        Product product,
        int? estimatedPortions)
    {
        PublicId = publicId;
        Name = name;
        CreatedAt = createdAt;
        Product = product;
        EstimatedPortions = estimatedPortions;
    }

    public Recipe(
        string name,
        Product product,
        int? estimatedPortions = null)
    {
        ArgumentNullException.ThrowIfNull(product, "Product is required");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name, "Recipe name is required");

        Product = product;
        PublicId = PublicIdHelper.Generate();
        Name = name;
        CreatedAt = DateTime.UtcNow;
        EstimatedPortions = estimatedPortions;
    }

    public void UpdateName(string name)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name, "Recipe name is required");

        Name = name;
    }

    public void UpdateEstimatedPortions(int? estimatedPortions)
    {
        EstimatedPortions = estimatedPortions;
    }

    public void AddIngredient(RecipeIngredient ingredient)
    {
        ArgumentNullException.ThrowIfNull(ingredient, "Recipe ingredient is required");

        _ingredients.Add(ingredient);
    }

    public void RemoveIngredient(RecipeIngredient ingredient)
    {
        ArgumentNullException.ThrowIfNull(ingredient, "Recipe ingredient is required");

        _ingredients.Remove(ingredient);
    }

    public void ClearIngredients()
    {
        _ingredients.Clear();
    }

    public static Recipe Rehydrate(
        string publicId,
        string name,
        DateTime createdAt,
        Product product,
        int? estimatedPortions,
        List<RecipeIngredient> ingredients)
    {
        var recipe = new Recipe(
            publicId,
            name,
            createdAt,
            product,
            estimatedPortions);

        foreach (var ingredient in ingredients)
        {
            recipe._ingredients.Add(ingredient);
        }

        return recipe;
    }
}







