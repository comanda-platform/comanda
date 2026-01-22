namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class RecipeEndpoints
{
    public static void MapRecipeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/recipes")
            .WithTags("Recipes")
            .RequireAuthorization();
        
        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get recipes with optional filters")
            .WithDescription("Retrieves recipes. Use query parameters to filter: productId");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get recipe by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new recipe");

        group.MapPost("/{publicId}/ingredients", AddIngredientAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Add an ingredient to a recipe");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchRecipeAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update recipe name");

        group.MapPatch("/{publicId}/ingredients/{ingredientPublicId}", UpdateIngredientQuantityAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update ingredient quantity in recipe");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}/ingredients/{ingredientPublicId}", RemoveIngredientAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Remove an ingredient from a recipe");

        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a recipe");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] RecipeQueryParameters query,
        RecipeUseCase UseCase)
    {
        // Apply filters based on query parameters
        if (!string.IsNullOrEmpty(query.ProductId))
        {
            var recipe = await UseCase.GetByProductPublicIdAsync(query.ProductId);
            return Results.Ok(RecipeResponseMapper.ToResponse(recipe));
        }
        else
        {
            var recipes = await UseCase.GetAllAsync();
            return Results.Ok(recipes.Select(RecipeResponseMapper.ToResponse));
        }
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        RecipeUseCase UseCase)
    {
        var recipe = await UseCase.GetByPublicIdAsync(publicId);

        return Results.Ok(RecipeResponseMapper.ToResponse(recipe));
    }

    private static async Task<IResult> CreateAsync(
        CreateRecipeRequest request,
        RecipeUseCase UseCase)
    {
        var recipe = await UseCase.CreateAsync(
            request.Name,
            request.ProductPublicId);

        return Results.Created(
            $"/api/recipes/{recipe.PublicId}",
            RecipeResponseMapper.ToResponse(recipe));
    }

    private static async Task<IResult> PatchRecipeAsync(
        string publicId,
        PatchRecipeRequest request,
        RecipeUseCase UseCase)
    {
        // Update name if provided
        if (!string.IsNullOrEmpty(request.Name))
        {
            await UseCase.UpdateNameAsync(publicId, request.Name);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> AddIngredientAsync(
        string publicId,
        AddRecipeIngredientRequest request,
        RecipeUseCase UseCase)
    {
        await UseCase.AddIngredientAsync(
            publicId,
            request.InventoryItemPublicId,
            request.UnitPublicId,
            request.Quantity);

        return Results.NoContent();
    }

    private static async Task<IResult> UpdateIngredientQuantityAsync(
        string publicId,
        string ingredientPublicId,
        UpdateRecipeIngredientRequest request,
        RecipeUseCase UseCase)
    {
        await UseCase.UpdateIngredientQuantityAsync(
            publicId,
            ingredientPublicId,
            request.Quantity);

        return Results.NoContent();
    }

    private static async Task<IResult> RemoveIngredientAsync(
        string publicId,
        string ingredientPublicId,
        RecipeUseCase UseCase)
    {
        await UseCase.RemoveIngredientAsync(publicId, ingredientPublicId);

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        RecipeUseCase UseCase)
    {
        await UseCase.DeleteAsync(publicId);

        return Results.NoContent();
    }
}







