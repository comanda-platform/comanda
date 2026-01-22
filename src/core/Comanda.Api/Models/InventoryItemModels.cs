namespace Comanda.Api.Models;

public record CreateInventoryItemRequest(
    string Name,
    string BaseUnitPublicId);

public record UpdateInventoryItemNameRequest(
    string Name);

public record UpdateInventoryItemBaseUnitRequest(
    string BaseUnitPublicId);

public record InventoryItemResponse(
    string PublicId,
    string Name,
    string BaseUnitPublicId,
    string BaseUnitCode,
    string BaseUnitName,
    DateTime CreatedAt);







