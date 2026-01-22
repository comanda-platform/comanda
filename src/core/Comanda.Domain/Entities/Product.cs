namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class Product
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal CurrentPrice { get; private set; }
    public ProductType Type { get; private set; }

    private readonly List<PriceHistoryEntry> _priceHistory = new();
    public IReadOnlyCollection<PriceHistoryEntry> PriceHistory => _priceHistory.AsReadOnly();

    private Product() { } // For reflection / serializers

    private Product(
        string publicId,
        string name,
        decimal currentPrice,
        ProductType type,
        string? description = null)
    {
        PublicId = publicId;
        Name = name;
        Description = description;
        CurrentPrice = currentPrice;
        Type = type;
    }

    public Product(
        string name,
        decimal initialPrice,
        ProductType type,
        string? description = null)
    {
        ArgumentNullException.ThrowIfNull(type, "Product type is required");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name, "Product name is required");

        if (initialPrice <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(initialPrice));

        PublicId = PublicIdHelper.Generate();
        Name = name;
        Type = type;
        Description = description;
        CurrentPrice = initialPrice;

        _priceHistory.Add(new PriceHistoryEntry(
            initialPrice,
            DateTime.UtcNow));
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(newPrice));

        if (newPrice == CurrentPrice)
            return; // No change needed

        // Close current price period
        {
            var currentEntry = _priceHistory.FirstOrDefault(p => p.EffectiveTo == null);

            currentEntry?.Close(DateTime.UtcNow);
        }

        // Add new price
        _priceHistory.Add(new PriceHistoryEntry(
            newPrice,
            DateTime.UtcNow));

        CurrentPrice = newPrice;
    }

    public void UpdateDetails(
        string name,
        string? description)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name, "Product name is required");

        Name = name;
        Description = description;
    }

    public decimal GetPriceOn(DateTime date) 
        => _priceHistory.FirstOrDefault(p => p.IsEffectiveOn(date))?.Price ?? CurrentPrice;

    public static Product Rehydrate(
        string publicId,
        string name,
        decimal currentPrice,
        ProductType type,
        string? description,
        List<PriceHistoryEntry> priceHistory)
    {
        var product = new Product(
            publicId,
            name,
            currentPrice,
            type,
            description);

        foreach (var entry in priceHistory)
        {
            product._priceHistory.Add(entry);
        }

        return product;
    }
}






