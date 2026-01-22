using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class ClientResponseMapper
{
    public static ClientResponse ToResponse(Domain.Entities.Client client)
        => new(
            client.PublicId,
            client.Name,
            client.ClientGroupPublicId,
            client.Contacts.Select(c => new ClientContactResponse(
                c.PublicId,
                c.Type,
                c.Value)));
}






