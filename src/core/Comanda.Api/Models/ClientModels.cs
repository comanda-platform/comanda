namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

public record CreateClientRequest(
    string Name,
    string? ClientGroupPublicId = null);

public record UpdateClientRequest(string Name);

public record AddClientContactRequest(
    ClientContactType Type,
    string Value);

public record ClientResponse(
    string PublicId,
    string Name,
    string? ClientGroupPublicId,
    IEnumerable<ClientContactResponse> Contacts);

public record ClientContactResponse(
    string PublicId,
    ClientContactType Type,
    string Value);







