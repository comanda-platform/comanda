using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public class NoteResponseMapper
{
    public static NoteResponse ToResponse(Domain.Entities.Note note)
        => new(
            note.PublicId,
            note.Content,
            note.ClientPublicId ?? string.Empty,
            note.ClientGroupPublicId ?? string.Empty,
            note.LocationPublicId ?? string.Empty,
            note.OrderPublicId ?? string.Empty,
            note.OrderLinePublicId ?? string.Empty,
            note.ProductPublicId ?? string.Empty,
            note.SidePublicId ?? string.Empty,
            note.CreatedByPublicId ?? string.Empty,
            note.CreatedAt);
}







