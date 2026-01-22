namespace Comanda.Domain.Entities;

public class OrderLineOption
{
    public string PublicId { get; set; }
    public string OptionKey { get; private set; }
    public bool IsIncluded { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Parent relationship
    internal int OrderLineId { get; private set; }

    private OrderLineOption() { } // For reflection / serializers

    private OrderLineOption(
        string publicId,
        string optionKey,
        bool isIncluded,
        DateTime createdAt,
        int orderLineId)
    {
        PublicId = publicId;
        OptionKey = optionKey;
        IsIncluded = isIncluded;
        CreatedAt = createdAt;
        OrderLineId = orderLineId;
    }

    public OrderLineOption(
        string optionKey,
        bool isIncluded = true)
    {
        if (string.IsNullOrWhiteSpace(optionKey))
            throw new ArgumentException("Option key is required", nameof(optionKey));

        OptionKey = optionKey;
        IsIncluded = isIncluded;
        CreatedAt = DateTime.UtcNow;
    }

    public void Include()
    {
        IsIncluded = true;
    }

    public void Exclude()
    {
        IsIncluded = false;
    }

    internal void SetOrderLineId(int orderLineId)
    {
        OrderLineId = orderLineId;
    }

    public static OrderLineOption Rehydrate(
        string publicId,
        string optionKey,
        bool isIncluded,
        DateTime createdAt,
        int orderLineId)
        => new(
            publicId,
            optionKey,
            isIncluded,
            createdAt,
            orderLineId);
}







