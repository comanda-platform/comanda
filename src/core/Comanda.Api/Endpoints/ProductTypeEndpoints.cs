namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Comanda.Domain.Entities;

public static class ProductTypeEndpoints
{
    public static void MapProductTypeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/product-types")
            .WithTags("Product Types")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync);
        group.MapGet("/{publicId}", GetByPublicIdAsync).AddEndpointFilter<RequirePublicIdFilter>();
        #endregion

        #region POST
        group.MapPost("/", CreateAsync);
        #endregion
    }

    private static async Task<IResult> GetAllAsync(ProductTypeUseCase UseCase)
    {
        var productTypes = await UseCase.GetAllAsync();

        return Results.Ok(productTypes.Select(ProductTypeResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        ProductTypeUseCase UseCase)
    {
        var productType = await UseCase.GetByPublicIdAsync(publicId);

        return Results.Ok(ProductTypeResponseMapper.ToResponse(productType));
    }

    private static async Task<IResult> CreateAsync(
        CreateProductTypeRequest request,
        ProductTypeUseCase UseCase)
    {
        var productType = await UseCase.CreateProductTypeAsync(request.Name);

        return Results.Created(
            $"/api/product-types/{productType.PublicId}",
            new ProductTypeResponse(
                productType.PublicId,
                productType.Name));
    }
}







