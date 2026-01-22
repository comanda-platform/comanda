using Comanda.Domain.Helpers;
using Comanda.Shared.Enums;

namespace Comanda.Domain.Entities;

public class PersonContact
{
    public string PublicId { get; private set; }
    public ClientContactType Type { get; private set; }
    public string Value { get; private set; }

    private PersonContact()
    {
        // For EF Core
        PublicId = string.Empty;
        Value = string.Empty;
    }

    private PersonContact(string publicId, ClientContactType type, string value)
    {
        PublicId = publicId;
        Type = type;
        Value = value;
    }

    public PersonContact(ClientContactType type, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Contact value is required", nameof(value));

        PublicId = PublicIdHelper.Generate();
        Type = type;
        Value = value;
    }

    public void UpdateValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Contact value is required", nameof(value));

        Value = value;
    }

    public static PersonContact Rehydrate(
        string publicId,
        ClientContactType type,
        string value)
        => new(publicId, type, value);
}
