namespace Comanda.Client.Kitchen.Infrastructure.ApiClients.ApiModels;

public record RecipeResponse(
    string PublicId,
    string Name,
    string ProductPublicId,
    int? EstimatedPortions,
    IEnumerable<RecipeIngredientResponse> Ingredients);

public record RecipeIngredientResponse(
    string PublicId,
    string InventoryItemPublicId,
    string InventoryItemName,
    string UnitCode,
    decimal Quantity);







