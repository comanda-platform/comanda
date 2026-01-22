namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class DailyMenu
{
    public string PublicId { get; private set; }
    public DateOnly Date { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string LocationPublicId { get; private set; }

    private readonly List<DailyMenuItem> _items = [];
    public IReadOnlyCollection<DailyMenuItem> Items => _items.AsReadOnly();

    private DailyMenu() { } // For reflection / serializers

    private DailyMenu(
        string publicId,
        DateOnly date,
        DateTime createdAt,
        string locationPublicId)
    {
        PublicId = publicId;
        Date = date;
        CreatedAt = createdAt;
        LocationPublicId = locationPublicId;
    }

    public DailyMenu(
        DateOnly date,
        string locationPublicId)
    {
        PublicId = PublicIdHelper.Generate();
        Date = date;
        LocationPublicId = locationPublicId;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(DailyMenuItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        _items.Add(item);
    }

    public void RemoveItem(DailyMenuItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        _items.Remove(item);
    }

    public void ClearItems()
    {
        _items.Clear();
    }

    public void ReorderItems()
    {
        var orderedItems = _items
            .OrderBy(i => i.SequenceOrder)
            .ToList();

        for (int i = 0; i < orderedItems.Count; i++)
        {
            orderedItems[i].UpdateSequenceOrder(i);
        }
    }

    public static DailyMenu Rehydrate(
        string publicId,
        DateOnly date,
        DateTime createdAt,
        string locationPublicId,
        List<DailyMenuItem> items)
    {
        var menu = new DailyMenu(
            publicId,
            date,
            createdAt,
            locationPublicId);

        foreach (var item in items)
        {
            menu._items.Add(item);
        }

        return menu;
    }
}







