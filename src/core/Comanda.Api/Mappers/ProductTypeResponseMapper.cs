using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class ProductTypeResponseMapper
{
    public static ProductTypeResponse ToResponse(Domain.Entities.ProductType productType) 
        => new(
            productType.PublicId,
            productType.Name);
}






