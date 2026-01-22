namespace Comanda.Domain.Entities;

public class InventoryPurchaseLine
{
    public string PublicId { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public InventoryItem Item { get; private set; }
    public Unit Unit { get; private set; }

    // Parent relationship
    internal string InventoryPurchasePublicId { get; private set; }

    private InventoryPurchaseLine() { } // For reflection / serializers

    private InventoryPurchaseLine(
        string publicId,
        decimal quantity,
        decimal unitPrice,
        DateTime createdAt,
        InventoryItem item,
        Unit unit,
        string inventoryPurchasePublicId)
    {
        PublicId = publicId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CreatedAt = createdAt;
        Item = item;
        Unit = unit;
        InventoryPurchasePublicId = inventoryPurchasePublicId;
    }

    public InventoryPurchaseLine(
        InventoryItem item,
        decimal quantity,
        decimal unitPrice,
        Unit unit)
    {
        ArgumentNullException.ThrowIfNull(item);
        ArgumentNullException.ThrowIfNull(unit);

        Item = item;
        Unit = unit;

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));

        Quantity = quantity;
        UnitPrice = unitPrice;
        CreatedAt = DateTime.UtcNow;
    }

    public decimal GetLineTotal() => Quantity * UnitPrice;

    public decimal GetQuantityInBaseUnit()
    {
        return Unit.ConvertToBase(Quantity);
    }

    public void UpdateQuantity(decimal quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        Quantity = quantity;
    }

    public void UpdateUnitPrice(decimal unitPrice)
    {
        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));

        UnitPrice = unitPrice;
    }

    internal void SetPurchaseId(string inventoryPurchasePublicId)
    {
        InventoryPurchasePublicId = inventoryPurchasePublicId;
    }

    public static InventoryPurchaseLine Rehydrate(
        string publicId,
        decimal quantity,
        decimal unitPrice,
        DateTime createdAt,
        InventoryItem item,
        Unit unit,
        string inventoryPurchasePublicId)
        => new(
            publicId,
            quantity,
            unitPrice,
            createdAt,
            item,
            unit,
            inventoryPurchasePublicId);
}







