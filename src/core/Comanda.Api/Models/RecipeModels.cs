namespace Comanda.Api.Models;

public record CreateRecipeRequest(
    string Name,
    string ProductPublicId);

public record UpdateRecipeNameRequest(
    string Name);

public record AddRecipeIngredientRequest(
    string InventoryItemPublicId,
    decimal Quantity,
    string UnitPublicId);

public record UpdateRecipeIngredientRequest(
    decimal Quantity);

public record RecipeResponse(
    string PublicId,
    string Name,
    string ProductPublicId,
    string ProductName,
    int? EstimatedPortions,
    IEnumerable<RecipeIngredientResponse> Ingredients);

public record RecipeIngredientResponse(
    string PublicId,
    string InventoryItemPublicId,
    string InventoryItemName,
    string UnitCode,
    decimal Quantity);







