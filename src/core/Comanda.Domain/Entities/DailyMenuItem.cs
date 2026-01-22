namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class DailyMenuItem
{
    public string PublicId { get; private set; }
    public int SequenceOrder { get; private set; }
    public string? OverriddenName { get; private set; }
    public decimal? OverriddenPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Product Product { get; private set; }

    // Parent relationship
    internal int DailyMenuId { get; private set; }

    private DailyMenuItem() { } // For reflection / serializers

    private DailyMenuItem(
        string publicId,
        int sequenceOrder,
        string? overriddenName,
        decimal? overriddenPrice,
        DateTime createdAt,
        Product product,
        int dailyMenuId)
    {
        PublicId = publicId;
        SequenceOrder = sequenceOrder;
        OverriddenName = overriddenName;
        OverriddenPrice = overriddenPrice;
        CreatedAt = createdAt;
        Product = product;
        DailyMenuId = dailyMenuId;
    }

    public DailyMenuItem(
        Product product,
        int sequenceOrder,
        string? overriddenName = null,
        decimal? overriddenPrice = null)
    {
        ArgumentNullException.ThrowIfNull(product);
        Product = product;

        if (sequenceOrder < 0)
            throw new ArgumentException("Sequence order cannot be negative", nameof(sequenceOrder));

        if (overriddenPrice.HasValue && overriddenPrice.Value < 0)
            throw new ArgumentException("Overridden price cannot be negative", nameof(overriddenPrice));

        PublicId = PublicIdHelper.Generate();
        SequenceOrder = sequenceOrder;
        OverriddenName = overriddenName;
        OverriddenPrice = overriddenPrice;
        CreatedAt = DateTime.UtcNow;
    }

    public string GetDisplayName() => OverriddenName ?? Product.Name;

    public decimal GetDisplayPrice() => OverriddenPrice ?? Product.CurrentPrice;

    public void UpdateSequenceOrder(int sequenceOrder)
    {
        if (sequenceOrder < 0)
            throw new ArgumentException("Sequence order cannot be negative", nameof(sequenceOrder));

        SequenceOrder = sequenceOrder;
    }

    public void SetOverriddenName(string? overriddenName)
    {
        OverriddenName = overriddenName;
    }

    public void SetOverriddenPrice(decimal? overriddenPrice)
    {
        if (overriddenPrice.HasValue && overriddenPrice.Value < 0)
            throw new ArgumentException("Overridden price cannot be negative", nameof(overriddenPrice));

        OverriddenPrice = overriddenPrice;
    }

    public void ClearOverrides()
    {
        OverriddenName = null;
        OverriddenPrice = null;
    }

    internal void SetMenuId(int menuId)
    {
        DailyMenuId = menuId;
    }

    public static DailyMenuItem Rehydrate(
        string publicId,
        int sequenceOrder,
        string? overriddenName,
        decimal? overriddenPrice,
        DateTime createdAt,
        Product product,
        int dailyMenuId)
        => new(
            publicId,
            sequenceOrder,
            overriddenName,
            overriddenPrice,
            createdAt,
            product,
            dailyMenuId);
}







