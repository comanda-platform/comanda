namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class DailySideAvailability
{
    public string PublicId { get; private set; }
    public DateOnly Date { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Side Side { get; private set; }

    private DailySideAvailability() { } // For reflection / serializers

    private DailySideAvailability(
        string publicId,
        DateOnly date,
        bool isAvailable,
        DateTime createdAt,
        Side side)
    {
        PublicId = publicId;
        Date = date;
        IsAvailable = isAvailable;
        CreatedAt = createdAt;
        Side = side;
    }

    public DailySideAvailability(
        Side side,
        DateOnly date,
        bool isAvailable = true)
    {
        ArgumentNullException.ThrowIfNull(side);

        PublicId = PublicIdHelper.Generate();
        Side = side;
        Date = date;
        IsAvailable = isAvailable;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetAvailable()
    {
        IsAvailable = true;
    }

    public void SetUnavailable()
    {
        IsAvailable = false;
    }

    public static DailySideAvailability Rehydrate(
        string publicId,
        DateOnly date,
        bool isAvailable,
        DateTime createdAt,
        Side side)
        => new(publicId,
               date,
               isAvailable,
               createdAt,
               side);
}







