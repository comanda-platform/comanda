namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class LedgerEntryMapper
{
    extension(ClientLedgerEntryDatabaseEntity dbEntity)
    {
        public LedgerEntry FromPersistence() 
            => LedgerEntry.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Client.PublicId,
                dbEntity.OccurredAt,
                dbEntity.Amount,
                (LedgerEntryType)dbEntity.LedgerEntryTypeId,
                dbEntity.PaymentMethodId.HasValue ? (PaymentMethod)dbEntity.PaymentMethodId.Value : null,
                dbEntity.OrderLine?.PublicId
            );
    }

    extension(LedgerEntry ledgerEntryDomainEntity)
    {
        public ClientLedgerEntryDatabaseEntity ToPersistence(ClientDatabaseEntity clientDbEntity) => 
            new()
            {
                PublicId = ledgerEntryDomainEntity.PublicId,
                //ClientId = domain.ClientId,  // TODO: To be set on the object in infrastructure layer 
                Client = clientDbEntity,
                OccurredAt = ledgerEntryDomainEntity.OccurredAt,
                Amount = ledgerEntryDomainEntity.Amount,
                LedgerEntryTypeId = (int)ledgerEntryDomainEntity.Type,
                PaymentMethodId = ledgerEntryDomainEntity.PaymentMethod.HasValue ? (int)ledgerEntryDomainEntity.PaymentMethod.Value : null,
                //OrderLineId = domain.OrderLineId, // TODO: To be set on the object in infrastructure layer 
                CreatedAt = DateTime.UtcNow
            };
    }
}







