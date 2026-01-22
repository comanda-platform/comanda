using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class PersonResponseMapper
{
    public static PersonResponse ToResponse(Domain.Entities.Person person)
        => new(
            person.PublicId,
            person.Name,
            person.Contacts.Select(c => new PersonContactResponse(
                c.PublicId,
                c.Type,
                c.Value)));
}
