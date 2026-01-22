namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Comanda.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

public static class UnitEndpoints
{
    public static void MapUnitEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/units")
            .WithTags("Units")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get units with optional filters")
            .WithDescription("Retrieves units. Use query parameters to filter: code, category");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get unit by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new unit");
        #endregion

        #region PUT
        group.MapPut("/{publicId}", UpdateAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update a unit");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a unit");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] UnitQueryParameters query,
        UnitUseCase UseCase)
    {
        // Apply filters based on query parameters
        if (!string.IsNullOrEmpty(query.Code))
        {
            var unit = await UseCase.GetByCodeAsync(query.Code);
            if (unit == null)
                return Results.NotFound();
            return Results.Ok(ToResponse(unit));
        }
        else if (query.Category.HasValue)
        {
            var units = await UseCase.GetByCategoryAsync(query.Category.Value);
            var response = units.Select(ToResponse);
            return Results.Ok(response);
        }
        else
        {
            var units = await UseCase.GetAllAsync();
            var response = units.Select(ToResponse);
            return Results.Ok(response);
        }
    }

    private static async Task<IResult> GetByPublicIdAsync(string publicId, UnitUseCase UseCase)
    {
        var unit = await UseCase.GetByPublicIdAsync(publicId);

        return Results.Ok(ToResponse(unit));
    }

    private static async Task<IResult> CreateAsync(CreateUnitRequest request, UnitUseCase UseCase)
    {
        var unit = await UseCase.CreateAsync(
            request.Code,
            request.Name,
            request.Category,
            request.ToBaseMultiplier);

        return Results.Created($"/api/units/{unit.PublicId}", ToResponse(unit));
    }

    private static async Task<IResult> UpdateAsync(
        string publicId,
        UpdateUnitRequest request,
        UnitUseCase UseCase)
    {
        await UseCase.UpdateAsync(
            publicId,
            request.Code,
            request.Name,
            request.Category,
            request.ToBaseMultiplier);

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        UnitUseCase UseCase)
    {
        await UseCase.DeleteAsync(publicId);

        return Results.NoContent();
    }

    private static UnitResponse ToResponse(Domain.Entities.Unit unit)
    {
        return new UnitResponse(
            unit.PublicId,
            unit.Code,
            unit.Name,
            unit.Category,
            unit.ToBaseMultiplier);
    }
}







