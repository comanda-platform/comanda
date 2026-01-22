namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class ClientGroupEndpoints
{
    public static void MapClientGroupEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/client-groups")
            .WithTags("Client Groups")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get client groups with optional filters")
            .WithDescription("Retrieves client groups. Use query parameters to filter: withCreditLine");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get client group by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new client group");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchClientGroupAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update client group fields")
            .WithDescription("Update client group name or credit line status");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a client group");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] ClientGroupQueryParameters query,
        ClientGroupUseCase UseCase)
    {
        IEnumerable<Domain.Entities.ClientGroup> groups;

        // Apply filters based on query parameters
        if (query.WithCreditLine.HasValue && query.WithCreditLine.Value)
        {
            groups = await UseCase.GetClientGroupsWithCreditLineAsync();
        }
        else
        {
            groups = await UseCase.GetAllClientGroupsAsync();
        }

        return Results.Ok(groups.Select(ClientGroupResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        ClientGroupUseCase UseCase)
    {
        var group = await UseCase.GetClientGroupByPublicIdAsync(publicId);

        return Results.Ok(ClientGroupResponseMapper.ToResponse(group));
    }

    private static async Task<IResult> CreateAsync(
        CreateClientGroupRequest request,
        ClientGroupUseCase UseCase)
    {
        var group = await UseCase.CreateClientGroupAsync(
            request.Name,
            request.HasCreditLine);

        return Results.Created(
            $"/api/client-groups/{group.PublicId}",
            ClientGroupResponseMapper.ToResponse(group));
    }

    private static async Task<IResult> PatchClientGroupAsync(
        string publicId,
        PatchClientGroupRequest request,
        ClientGroupUseCase UseCase)
    {
        // Update name if provided
        if (!string.IsNullOrEmpty(request.Name))
        {
            await UseCase.UpdateClientGroupNameAsync(publicId, request.Name);
        }

        // Update credit line status if provided
        if (request.HasCreditLine.HasValue)
        {
            if (request.HasCreditLine.Value)
            {
                await UseCase.EnableCreditLineAsync(publicId);
            }
            else
            {
                await UseCase.DisableCreditLineAsync(publicId);
            }
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        ClientGroupUseCase UseCase)
    {
        await UseCase.DeleteClientGroupAsync(publicId);

        return Results.NoContent();
    }
}







