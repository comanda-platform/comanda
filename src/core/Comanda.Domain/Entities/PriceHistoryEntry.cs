namespace Comanda.Domain.Entities;

public class PriceHistoryEntry
{
    public string PublicId { get; private set; }
    public decimal Price { get; private set; }
    public DateTime EffectiveFrom { get; private set; }
    public DateTime? EffectiveTo { get; private set; }

    private PriceHistoryEntry() { } // For reflection / serializers

    private PriceHistoryEntry(
        string publicId,
        decimal price,
        DateTime effectiveFrom,
        DateTime? effectiveTo)
    {
        PublicId = publicId;
        Price = price;
        EffectiveFrom = effectiveFrom;
        EffectiveTo = effectiveTo;
    }

    public PriceHistoryEntry(
        decimal price,
        DateTime effectiveFrom)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));

        Price = price;
        EffectiveFrom = effectiveFrom;
    }

    public bool IsEffectiveOn(DateTime date)
        => EffectiveFrom <= date && (EffectiveTo == null || EffectiveTo > date);

    public bool IsCurrentlyEffective()
        => EffectiveTo == null;

    public void Close(DateTime closedAt)
    {
        if (EffectiveTo != null)
            throw new InvalidOperationException("Price period already closed");

        EffectiveTo = closedAt;
    }

    public static PriceHistoryEntry Rehydrate(
        string publicId,
        decimal price,
        DateTime effectiveFrom,
        DateTime? effectiveTo)
        => new (
            publicId,
            price,
            effectiveFrom,
            effectiveTo);
}






