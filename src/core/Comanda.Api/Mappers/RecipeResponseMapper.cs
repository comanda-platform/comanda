using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class RecipeResponseMapper
{
    public static RecipeResponse ToResponse(Domain.Entities.Recipe recipe) 
        => new(
            recipe.PublicId,
            recipe.Name,
            recipe.Product.PublicId,
            recipe.Product.Name,
            recipe.EstimatedPortions,
            recipe.Ingredients.Select(i => new RecipeIngredientResponse(
                i.Item.PublicId,
                i.Item.PublicId,
                i.Item.Name,
                i.Unit.Code,
                i.Quantity)));
}






