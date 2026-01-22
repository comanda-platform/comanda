namespace Comanda.Api.Models;

public record CreateProductRequest(
    string Name,
    decimal Price,
    string ProductTypePublicId,
    string? Description = null);

public record UpdateProductRequest(
    string Name,
    string? Description = null);

public record UpdateProductPriceRequest(decimal Price);

public record ProductResponse(
    string PublicId,
    string Name,
    string? Description,
    decimal CurrentPrice,
    string ProductTypePublicId,
    string ProductTypeName);







