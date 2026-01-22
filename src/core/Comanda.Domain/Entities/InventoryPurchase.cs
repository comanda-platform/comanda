namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;
using Comanda.Domain.Helpers;

public class InventoryPurchase
{
    public string PublicId { get; private set; }
    public DateTime PurchasedAt { get; private set; }
    public decimal TotalAmount { get; private set; }
    public InventoryPurchaseType PurchaseType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public Supplier Supplier { get; private set; }
    public string? StoreLocationPublicId { get; private set; }

    private readonly List<InventoryPurchaseLine> _lines = new();
    public IReadOnlyCollection<InventoryPurchaseLine> Lines => _lines.AsReadOnly();

    private InventoryPurchase() { } // For reflection / serializers

    private InventoryPurchase(
        string publicId,
        DateTime purchasedAt,
        decimal totalAmount,
        InventoryPurchaseType purchaseType,
        DateTime createdAt,
        DateTime? deliveredAt,
        Supplier supplier,
        string? storeLocationPublicId)
    {
        PublicId = publicId;
        PurchasedAt = purchasedAt;
        TotalAmount = totalAmount;
        PurchaseType = purchaseType;
        CreatedAt = createdAt;
        DeliveredAt = deliveredAt;
        Supplier = supplier;
        StoreLocationPublicId = storeLocationPublicId;
    }

    public InventoryPurchase(
        Supplier supplier,
        InventoryPurchaseType purchaseType,
        DateTime? purchasedAt = null,
        string? storeLocationPublicId = null)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        Supplier = supplier;
        PublicId = PublicIdHelper.Generate();
        PurchasedAt = purchasedAt ?? DateTime.UtcNow;
        TotalAmount = 0;
        PurchaseType = purchaseType;
        CreatedAt = DateTime.UtcNow;
        StoreLocationPublicId = storeLocationPublicId;
    }

    public void AddLine(InventoryPurchaseLine line)
    {
        ArgumentNullException.ThrowIfNull(line);

        _lines.Add(line);

        RecalculateTotal();
    }

    public void RemoveLine(InventoryPurchaseLine line)
    {
        ArgumentNullException.ThrowIfNull(line);

        _lines.Remove(line);

        RecalculateTotal();
    }

    public void MarkAsDelivered(DateTime? deliveredAt = null)
    {
        if (DeliveredAt.HasValue)
            throw new InvalidOperationException("Purchase has already been marked as delivered");

        DeliveredAt = deliveredAt ?? DateTime.UtcNow;
    }

    public void UpdatePurchaseType(InventoryPurchaseType purchaseType)
    {
        PurchaseType = purchaseType;
    }

    public void UpdateStoreLocation(string? storeLocationPublicId)
    {
        StoreLocationPublicId = storeLocationPublicId;
    }

    public void RecalculateTotal()
    {
        TotalAmount = _lines.Sum(l => l.GetLineTotal());
    }

    public static InventoryPurchase Rehydrate(
        string publicId,
        DateTime purchasedAt,
        decimal totalAmount,
        InventoryPurchaseType purchaseType,
        DateTime createdAt,
        DateTime? deliveredAt,
        string? storeLocationPublicId,
        Supplier supplier,
        List<InventoryPurchaseLine> lines)
    {
        var purchase = new InventoryPurchase(
            publicId,
            purchasedAt,
            totalAmount,
            purchaseType,
            createdAt,
            deliveredAt,
            supplier,
            storeLocationPublicId);

        foreach (var line in lines)
        {
            purchase._lines.Add(line);
        }

        return purchase;
    }
}







