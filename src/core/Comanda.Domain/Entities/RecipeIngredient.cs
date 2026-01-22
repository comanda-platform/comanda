namespace Comanda.Domain.Entities;

public class RecipeIngredient
{
    public string PublicId { get; private set; }
    public decimal Quantity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public InventoryItem Item { get; private set; }
    public Unit Unit { get; private set; }

    // Parent relationship
    public string RecipePublicId { get; private set; }

    private RecipeIngredient() { } // For reflection / serializers

    private RecipeIngredient(
        string publicId,
        decimal quantity,
        DateTime createdAt,
        InventoryItem item,
        Unit unit,
        string  recipePublicId)
    {
        PublicId = publicId;
        Quantity = quantity;
        CreatedAt = createdAt;
        Item = item;
        Unit = unit;
        RecipePublicId = recipePublicId;
    }

    public RecipeIngredient(
        InventoryItem item,
        decimal quantity,
        Unit unit)
    {
        ArgumentNullException.ThrowIfNull(item, "Item is required");
        ArgumentNullException.ThrowIfNull(unit, "Unit is required");
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0, "Quantity must be greater than zero");

        Item = item;
        Unit = unit;
        Quantity = quantity;
        CreatedAt = DateTime.UtcNow;
    }

    public decimal GetQuantityInBaseUnit()
    {
        return Unit.ConvertToBase(Quantity);
    }

    public void UpdateQuantity(decimal quantity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0, "Quantity must be greater than zero");

        Quantity = quantity;
    }

    public void UpdateUnit(Unit unit)
    {
        ArgumentNullException.ThrowIfNull(unit, "Unit is required");

        Unit = unit;
    }

    internal void SetRecipeId(string recipePublicId)
    {
        RecipePublicId = recipePublicId;
    }

    public static RecipeIngredient Rehydrate(
        string publicId,
        decimal quantity,
        DateTime createdAt,
        InventoryItem item,
        Unit unit,
        string recipePublicId)
        => new(
            publicId,
            quantity,
            createdAt,
            item,
            unit,
            recipePublicId);
}







