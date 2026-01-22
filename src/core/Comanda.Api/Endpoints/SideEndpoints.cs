namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class SideEndpoints
{
    public static void MapSideEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sides")
            .WithTags("Sides")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get sides with optional filters")
            .WithDescription("Retrieves sides. Use query parameters to filter: active");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get side by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new side");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchSideAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update side fields or status")
            .WithDescription("Update side name or activate/deactivate");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a side");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] SideQueryParameters query,
        SideUseCase UseCase)
    {
        IEnumerable<Domain.Entities.Side> sides;

        // Apply filters based on query parameters
        if (query.Active.HasValue && query.Active.Value)
        {
            sides = await UseCase.GetActiveAsync();
        }
        else
        {
            sides = await UseCase.GetAllAsync();
        }

        return Results.Ok(sides.Select(SideResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        SideUseCase UseCase)
    {
        var side = await UseCase.GetByPublicIdAsync(publicId);
        
        return Results.Ok(SideResponseMapper.ToResponse(side));
    }

    private static async Task<IResult> CreateAsync(
        CreateSideRequest request,
        SideUseCase UseCase)
    {
        var side = await UseCase.CreateAsync(request.Name);

        return Results.Created(
            $"/api/sides/{side.PublicId}",
            SideResponseMapper.ToResponse(side));
    }

    private static async Task<IResult> PatchSideAsync(
        string publicId,
        PatchSideRequest request,
        SideUseCase UseCase)
    {
        // Update name if provided
        if (!string.IsNullOrEmpty(request.Name))
        {
            await UseCase.UpdateNameAsync(publicId, request.Name);
        }

        // Update active status if provided
        if (request.IsActive.HasValue)
        {
            if (request.IsActive.Value)
            {
                await UseCase.ActivateAsync(publicId);
            }
            else
            {
                await UseCase.DeactivateAsync(publicId);
            }
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        SideUseCase UseCase)
    {
        await UseCase.DeleteAsync(publicId);

        return Results.NoContent();
    }
}







