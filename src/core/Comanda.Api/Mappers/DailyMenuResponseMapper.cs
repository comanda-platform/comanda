using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class DailyMenuResponseMapper
{
    public static DailyMenuResponse ToResponse(Domain.Entities.DailyMenu menu) 
        => new(
            menu.PublicId,
            menu.Date,
            menu.LocationPublicId,
            menu.Items.Select(i => new DailyMenuItemResponse(
                i.PublicId,
                i.Product.PublicId,
                i.Product.Name,
                i.SequenceOrder,
                i.OverriddenName,
                i.OverriddenPrice,
                i.GetDisplayPrice())),
            menu.CreatedAt);
}






