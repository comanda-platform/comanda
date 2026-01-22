namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

public record CreatePersonRequest(string Name);

public record UpdatePersonRequest(string Name);

public record AddPersonContactRequest(
    ClientContactType Type,
    string Value);

public record PersonResponse(
    string PublicId,
    string Name,
    IEnumerable<PersonContactResponse> Contacts);

public record PersonContactResponse(
    string PublicId,
    ClientContactType Type,
    string Value);
