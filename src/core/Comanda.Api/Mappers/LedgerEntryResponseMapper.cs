using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class LedgerEntryResponseMapper
{
    public static LedgerEntryResponse ToResponse(Domain.Entities.LedgerEntry entry)
        => new(
            entry.PublicId,
            entry.Type,
            entry.Amount,
            entry.PaymentMethod,
            entry.ClientPublicId ?? string.Empty,
            entry.OrderLinePublicId ?? string.Empty,
            entry.OccurredAt);
}






