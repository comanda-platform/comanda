namespace Comanda.Api.Models;

/// <summary>
/// Request model for patching a recipe
/// </summary>
public record PatchRecipeRequest
{
    public string? Name { get; init; }
}

/// <summary>
/// Query parameters for filtering recipes
/// </summary>
public record RecipeQueryParameters
{
    public string? ProductId { get; init; }
}







