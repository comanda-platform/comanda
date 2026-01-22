namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;
using System;

public class ClientGroup
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }
    public bool HasCreditLine { get; private set; }

    private readonly List<Client> _members = [];
    public IReadOnlyCollection<Client> Members => _members.AsReadOnly();

    private readonly List<Location> _locations = [];
    public IReadOnlyCollection<Location> Locations => _locations.AsReadOnly();

    private ClientGroup() { } // For reflection / serializers

    private ClientGroup(
        string publicId,
        string name,
        bool hasCreditLine)
    {
        PublicId = publicId;
        Name = name;
        HasCreditLine = hasCreditLine;
    }

    public ClientGroup(
        string name,
        bool hasCreditLine = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Client group name is required", nameof(name));

        PublicId = PublicIdHelper.Generate();
        Name = name;
        HasCreditLine = hasCreditLine;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Client group name is required", nameof(name));

        Name = name;
    }

    public void EnableCreditLine()
    {
        if (HasCreditLine)
            throw new InvalidOperationException("Credit line is already enabled");

        HasCreditLine = true;
    }

    public void DisableCreditLine()
    {
        if (!HasCreditLine)
            throw new InvalidOperationException("Credit line is already disabled");

        HasCreditLine = false;
    }

    public void AddMember(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);

        if (_members.Any(m => m.PublicId == client.PublicId))
            throw new InvalidOperationException("Client is already a member of this group");

        _members.Add(client);
    }

    public void RemoveMember(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);

        var existingMember = _members.FirstOrDefault(m => m.PublicId == client.PublicId)
            ?? throw new InvalidOperationException("Client is not a member of this group");

        _members.Remove(existingMember);
    }

    public static ClientGroup Rehydrate(
        string publicId,
        string name,
        bool hasCreditLine,
        List<Client> members,
        List<Location> locations)
    {
        var group = new ClientGroup(
            publicId,
            name,
            hasCreditLine);

        foreach (var member in members)
        {
            group._members.Add(member);
        }

        foreach (var location in locations)
        {
            group._locations.Add(location);
        }

        return group;
    }
}







