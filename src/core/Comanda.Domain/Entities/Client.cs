namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class Client
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }
    public string? ClientGroupPublicId { get; private set; }

    private readonly List<ClientContact> _contacts = new();
    public IReadOnlyCollection<ClientContact> Contacts => _contacts.AsReadOnly();

    private readonly List<Location> _locations = new();
    public IReadOnlyCollection<Location> Locations => _locations.AsReadOnly();

    private Client() { } // For reflection / serializers

    private Client(
        string publicId,
        string name,
        string? clientGroupPublicId)
    {
        PublicId = publicId;
        Name = name;
        ClientGroupPublicId = clientGroupPublicId;
    }

    public Client(
        string name,
        ClientGroup? clientGroup = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Client name is required", nameof(name));

        PublicId = PublicIdHelper.Generate();
        Name = name;

        if (clientGroup != null)
        {
            ClientGroupPublicId = clientGroup.PublicId;
        }
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Client name is required", nameof(name));

        Name = name;
    }

    public void AssignToGroup(ClientGroup clientGroup)
    {
        ArgumentNullException.ThrowIfNull(clientGroup);

        ClientGroupPublicId = clientGroup.PublicId;
    }

    public void RemoveFromGroup()
    {
        if (ClientGroupPublicId == null)
            throw new InvalidOperationException("Client is not assigned to any group");

        ClientGroupPublicId = null;
    }

    public void AddContact(ClientContact contact)
    {
        if (contact == null)
            throw new ArgumentNullException(nameof(contact));

        _contacts.Add(contact);
    }

    public void RemoveContact(ClientContact contact)
    {
        if (contact == null)
            throw new ArgumentNullException(nameof(contact));

        if (!_contacts.Remove(contact))
            throw new InvalidOperationException("Contact not found");
    }

    public static Client Rehydrate(
        string publicId,
        string name,
        string? clientGroupPublicId,
        List<ClientContact> contacts,
        List<Location> locations)
    {
        var client = new Client(
            publicId,
            name,
            clientGroupPublicId);

        foreach (var contact in contacts)
        {
            client._contacts.Add(contact);
        }

        foreach (var location in locations)
        {
            client._locations.Add(location);
        }

        return client;
    }
}







