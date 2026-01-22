namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class Side
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Side() { } // For reflection / serializers

    private Side(
        string publicId,
        string name,
        bool isActive,
        DateTime createdAt)
    {
        PublicId = publicId;
        Name = name;
        IsActive = isActive;
        CreatedAt = createdAt;
    }

    public Side(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, "Side name is required");

        PublicId = PublicIdHelper.Generate();
        Name = name;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, "Side name is required");

        Name = name;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidOperationException("Side is already active");

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("Side is already inactive");

        IsActive = false;
    }

    public static Side Rehydrate(
        string publicId,
        string name,
        bool isActive,
        DateTime createdAt)
        => new(
            publicId,
            name,
            isActive,
            createdAt);
}







