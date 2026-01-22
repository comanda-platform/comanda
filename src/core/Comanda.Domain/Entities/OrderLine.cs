namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;

public class OrderLine
{
    public string PublicId { get; private set; }
    public string OrderPublicId { get; private set; }
    public string ProductPublicId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string? ClientPublicId { get; private set; }
    
    // Prep tracking fields
    public OrderLinePrepStatus PrepStatus { get; private set; }
    public DateTime? PrepStartedAt { get; private set; }
    public DateTime? PrepCompletedAt { get; private set; }
    public string? ContainerType { get; private set; }
    
    private List<string>? _selectedSides;
    public IReadOnlyList<string>? SelectedSides => _selectedSides?.AsReadOnly();

    private OrderLine() { } // For reflection / serializers

    private OrderLine(
        string publicId,
        string orderPublicId,
        string productPublicId,
        int quantity,
        decimal unitPrice,
        string? clientPublicId,
        OrderLinePrepStatus prepStatus,
        DateTime? prepStartedAt,
        DateTime? prepCompletedAt,
        string? containerType,
        List<string>? selectedSides)
    {
        PublicId = publicId;
        OrderPublicId = orderPublicId;
        ProductPublicId = productPublicId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        ClientPublicId = clientPublicId;
        PrepStatus = prepStatus;
        PrepStartedAt = prepStartedAt;
        PrepCompletedAt = prepCompletedAt;
        ContainerType = containerType;
        _selectedSides = selectedSides;
    }

    // Domain constructor using public ids
    public OrderLine(
        string productPublicId,
        int quantity,
        decimal unitPrice,
        string? clientPublicId = null,
        string? containerType = null,
        List<string>? selectedSides = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));

        ProductPublicId = productPublicId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        ClientPublicId = clientPublicId;
        ContainerType = containerType;
        _selectedSides = selectedSides;
        PrepStatus = OrderLinePrepStatus.Pending;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        Quantity = quantity;
    }

    public void AssignToClientByPublicId(string clientPublicId)
    {
        ClientPublicId = clientPublicId;
    }

    public void UnassignFromClient()
    {
        ClientPublicId = null;
    }
    
    /// <summary>
    /// Start preparation of this order line
    /// </summary>
    public void StartPrep()
    {
        if (PrepStatus != OrderLinePrepStatus.Pending)
            throw new InvalidOperationException($"Cannot start prep for line in status {PrepStatus}");
        
        PrepStatus = OrderLinePrepStatus.InProgress;
        PrepStartedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Mark this line as plated (container and sides already set at order creation)
    /// </summary>
    public void CompletePlating()
    {
        if (PrepStatus != OrderLinePrepStatus.InProgress && PrepStatus != OrderLinePrepStatus.Pending)
            throw new InvalidOperationException($"Cannot complete plating for line in status {PrepStatus}");

        PrepStatus = OrderLinePrepStatus.Plated;
        PrepCompletedAt = DateTime.UtcNow;

        // Set started time if we skipped directly from pending to plated
        PrepStartedAt ??= PrepCompletedAt;
    }
    
    /// <summary>
    /// Mark this line as fully completed (order has been delivered/picked up)
    /// </summary>
    public void MarkCompleted()
    {
        if (PrepStatus != OrderLinePrepStatus.Plated)
            throw new InvalidOperationException($"Cannot mark line as completed when in status {PrepStatus}");
        
        PrepStatus = OrderLinePrepStatus.Completed;
    }
    
    /// <summary>
    /// Reset prep status back to pending (e.g., if order is rejected)
    /// </summary>
    public void ResetPrep()
    {
        PrepStatus = OrderLinePrepStatus.Pending;
        PrepStartedAt = null;
        PrepCompletedAt = null;
        ContainerType = null;
        _selectedSides = null;
    }

    public decimal LineTotal => Quantity * UnitPrice;
    
    public bool IsPrepped => PrepStatus == OrderLinePrepStatus.Plated || PrepStatus == OrderLinePrepStatus.Completed;

    // DB-compatible factory
    public static OrderLine Rehydrate(
        string publicId,
        string orderPublicId,
        string productPublicId,
        int quantity,
        decimal unitPrice,
        string? clientPublicId,
        OrderLinePrepStatus prepStatus = OrderLinePrepStatus.Pending,
        DateTime? prepStartedAt = null,
        DateTime? prepCompletedAt = null,
        string? containerType = null,
        List<string>? selectedSides = null)
        => new(
            publicId,
            orderPublicId,
            productPublicId,
            quantity,
            unitPrice,
            clientPublicId,
            prepStatus,
            prepStartedAt,
            prepCompletedAt,
            containerType,
            selectedSides);
}







