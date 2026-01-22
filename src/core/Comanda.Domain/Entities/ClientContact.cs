namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;

public class ClientContact
{
    public string PublicId { get; private set; }
    public ClientContactType Type { get; private set; }
    public string Value { get; private set; }

    private ClientContact() { } // For reflection / serializers

    private ClientContact(
        string publicId,
        ClientContactType type,
        string value)
    {
        PublicId = publicId;
        Type = type;
        Value = value;
    }

    public ClientContact(
        ClientContactType type,
        string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Client contact type value is required", nameof(value));

        Type = type;
        Value = value;
    }

    public static ClientContact Rehydrate(
        string publicId,
        ClientContactType type,
        string value)
        => new(
            publicId,
            type,
            value);
}







