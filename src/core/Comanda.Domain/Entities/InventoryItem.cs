namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class InventoryItem
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Unit BaseUnit { get; private set; }

    private InventoryItem() { } // For reflection / serializers

    private InventoryItem(
        string publicId,
        string name,
        DateTime createdAt,
        Unit baseUnit)
    {
        PublicId = publicId;
        Name = name;
        CreatedAt = createdAt;
        BaseUnit = baseUnit;
    }

    public InventoryItem(
        string name,
        Unit baseUnit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Inventory item name is required", nameof(name));

        ArgumentNullException.ThrowIfNull(baseUnit);
        BaseUnit = baseUnit;

        PublicId = PublicIdHelper.Generate();
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Inventory item name is required", nameof(name));

        Name = name;
    }

    public void UpdateBaseUnit(Unit baseUnit)
    {
        ArgumentNullException.ThrowIfNull(baseUnit);
        BaseUnit = baseUnit;
    }

    public static InventoryItem Rehydrate(
        string publicId,
        string name,
        DateTime createdAt,
        Unit baseUnit)
        => new(
            publicId,
            name,
            createdAt,
            baseUnit);
}







