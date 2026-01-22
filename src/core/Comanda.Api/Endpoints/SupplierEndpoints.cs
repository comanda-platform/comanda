namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Comanda.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

public static class SupplierEndpoints
{
    public static void MapSupplierEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/suppliers")
            .WithTags("Suppliers")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get suppliers with optional filters")
            .WithDescription("Retrieves suppliers. Use query parameters to filter: type");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get supplier by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new supplier");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchSupplierAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update supplier fields")
            .WithDescription("Update supplier name or type");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a supplier");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] SupplierQueryParameters query,
        SupplierUseCase UseCase)
    {
        IEnumerable<Domain.Entities.Supplier> suppliers;

        // Apply filters based on query parameters
        if (query.Type.HasValue)
        {
            suppliers = await UseCase.GetByTypeAsync(query.Type.Value);
        }
        else
        {
            suppliers = await UseCase.GetAllAsync();
        }

        var response = suppliers.Select(ToResponse);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetByPublicIdAsync(string publicId, SupplierUseCase UseCase)
    {
        var supplier = await UseCase.GetByPublicIdAsync(publicId);
        if (supplier == null)
            return Results.NotFound();

        return Results.Ok(ToResponse(supplier));
    }

    private static async Task<IResult> CreateAsync(CreateSupplierRequest request, SupplierUseCase UseCase)
    {
        var supplier = await UseCase.CreateAsync(request.Name, request.Type);
        return Results.Created($"/api/suppliers/{supplier.PublicId}", ToResponse(supplier));
    }

    private static async Task<IResult> PatchSupplierAsync(
        string publicId,
        PatchSupplierRequest request,
        SupplierUseCase UseCase)
    {
        // Update name if provided
        if (!string.IsNullOrEmpty(request.Name))
        {
            await UseCase.UpdateNameAsync(publicId, request.Name);
        }

        // Update type if provided
        if (request.Type.HasValue)
        {
            await UseCase.UpdateTypeAsync(publicId, request.Type.Value);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(string publicId, SupplierUseCase UseCase)
    {
        await UseCase.DeleteAsync(publicId);
        return Results.NoContent();
    }

    private static SupplierResponse ToResponse(Domain.Entities.Supplier supplier)
    {
        return new SupplierResponse(
            supplier.PublicId,
            supplier.Name,
            supplier.Type,
            supplier.CreatedAt);
    }
}







