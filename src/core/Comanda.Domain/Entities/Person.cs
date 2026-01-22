using Comanda.Domain.Helpers;

namespace Comanda.Domain.Entities;

public class Person
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }

    private readonly List<PersonContact> _contacts = new();
    public IReadOnlyCollection<PersonContact> Contacts => _contacts.AsReadOnly();

    private Person()
    {
        // For EF Core
        PublicId = string.Empty;
        Name = string.Empty;
    }

    private Person(string publicId, string name)
    {
        PublicId = publicId;
        Name = name;
    }

    public Person(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Person name is required", nameof(name));

        PublicId = PublicIdHelper.Generate();
        Name = name;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Person name is required", nameof(name));

        Name = name;
    }

    public void AddContact(PersonContact contact)
    {
        ArgumentNullException.ThrowIfNull(contact);
        _contacts.Add(contact);
    }

    public void RemoveContact(PersonContact contact)
    {
        ArgumentNullException.ThrowIfNull(contact);

        if (!_contacts.Remove(contact))
            throw new InvalidOperationException("Contact not found");
    }

    public static Person Rehydrate(
        string publicId,
        string name,
        List<PersonContact> contacts)
    {
        var person = new Person(publicId, name);

        foreach (var contact in contacts)
            person._contacts.Add(contact);

        return person;
    }
}
