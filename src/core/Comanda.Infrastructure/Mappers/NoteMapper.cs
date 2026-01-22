namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class NoteMapper
{
    extension(NoteDatabaseEntity dbEntity)
    {
        public Note FromPersistence() => 
            Note.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Content,
                dbEntity.CreatedAt,
                dbEntity.CreatedBy?.PublicId,
                dbEntity.Client?.PublicId,
                dbEntity.ClientGroup?.PublicId,
                dbEntity.Location?.PublicId,
                dbEntity.Order?.PublicId,
                dbEntity.OrderLine?.PublicId,
                dbEntity.Product?.PublicId,
                dbEntity.Side?.PublicId
            );
    }

    extension(Note domainEntity)
    {
        public NoteDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Content = domainEntity.Content,
                //CreatedAt = domain.CreatedAt, // TODO: To be set on the object in infrastructure layer 
                //CreatedById = domain.CreatedById,
                //ClientId = domain.ClientId,
                //ClientGroupId = domain.ClientGroupId,
                //LocationId = domain.LocationId,
                //OrderId = domain.OrderId,
                //OrderLineId = domain.OrderLineId,
                //ProductId = domain.ProductId,
                //SideId = domain.SideId
            };
    }
}







