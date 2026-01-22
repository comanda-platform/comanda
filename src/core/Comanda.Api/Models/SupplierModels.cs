namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

public record CreateSupplierRequest(
    string Name,
    SupplierType Type);

public record UpdateSupplierNameRequest(
    string Name);

public record UpdateSupplierTypeRequest(
    SupplierType Type);

public record SupplierResponse(
    string PublicId,
    string Name,
    SupplierType Type,
    DateTime CreatedAt);







