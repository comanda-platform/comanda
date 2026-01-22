namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get products with optional filters")
            .WithDescription("Retrieves products. Use query parameters to filter: typeId");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get product by ID");

        group.MapGet("/{publicId}/price-on-date", GetPriceOnDateAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get product price on specific date");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new product");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchProductAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update product fields")
            .WithDescription("Update product name, description, or price");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a product");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] ProductQueryParameters query,
        ProductUseCase UseCase)
    {
        IEnumerable<Domain.Entities.Product> products;

        // Apply filters based on query parameters
        if (!string.IsNullOrEmpty(query.TypeId))
        {
            products = await UseCase.GetProductsByTypeAsync(query.TypeId);
        }
        else
        {
            products = await UseCase.GetAllProductsAsync();
        }

        return Results.Ok(products.Select(ProductResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        ProductUseCase UseCase)
    {
        var product = await UseCase.GetProductByPublicIdAsync(publicId);

        return Results.Ok(ProductResponseMapper.ToResponse(product));
    }

    private static async Task<IResult> CreateAsync(
        CreateProductRequest request,
        ProductUseCase UseCase)
    {
        var product = await UseCase.CreateProductAsync(
            request.Name,
            request.Price,
            request.ProductTypePublicId,
            request.Description);

        return Results.Created(
            $"/api/products/{product.PublicId}",
            ProductResponseMapper.ToResponse(product));
    }

    private static async Task<IResult> PatchProductAsync(
        string publicId,
        PatchProductRequest request,
        ProductUseCase UseCase)
    {
        // Update name or description if provided
        if (!string.IsNullOrEmpty(request.Name) || !string.IsNullOrEmpty(request.Description))
        {
            await UseCase.UpdateProductAsync(
                publicId,
                request.Name,
                request.Description);
        }

        // Update price if provided
        if (request.Price.HasValue)
        {
            await UseCase.UpdateProductPriceAsync(publicId, request.Price.Value);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> GetPriceOnDateAsync(
        string publicId,
        DateTime date,
        ProductUseCase UseCase)
    {
        var price = await UseCase.GetProductPriceOnDateAsync(
            publicId,
            date);

        return Results.Ok(new { price });
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        ProductUseCase UseCase)
    {
        await UseCase.DeleteProductAsync(publicId);

        return Results.NoContent();
    }
}







