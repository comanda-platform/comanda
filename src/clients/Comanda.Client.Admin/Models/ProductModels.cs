namespace Comanda.Client.Admin.Models;

public record ProductResponse(
    string PublicId,
    string Name,
    string? Description,
    decimal CurrentPrice,
    ProductTypeResponse Type,
    IEnumerable<PriceHistoryEntryResponse> PriceHistory);

public record ProductTypeResponse(
    string PublicId,
    string Name);

public record PriceHistoryEntryResponse(
    string PublicId,
    decimal Price,
    DateTime EffectiveFrom,
    DateTime? EffectiveTo);

public record CreateProductRequest(
    string Name,
    decimal InitialPrice,
    string ProductTypePublicId,
    string? Description);

public record UpdateProductRequest(
    string Name,
    string? Description);

public record UpdateProductPriceRequest(
    decimal NewPrice);

public record CreateProductTypeRequest(
    string Name);










