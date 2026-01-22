using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class ProductResponseMapper
{
    public static ProductResponse ToResponse(Domain.Entities.Product product) 
        => new(
            product.PublicId,
            product.Name,
            product.Description,
            product.CurrentPrice,
            product.Type.PublicId,
            product.Type.Name);
}






