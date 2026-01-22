namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class ClientEndpoints
{
    public static void MapClientEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clients")
            .WithTags("Clients")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get clients with optional filters")
            .WithDescription("Retrieves clients. Use query parameters to filter: groupId");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get client by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new client");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchClientAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update client fields")
            .WithDescription("Update client name or group assignment. Pass null for clientGroupPublicId to remove from group");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a client");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] ClientQueryParameters query,
        ClientUseCase UseCase)
    {
        IEnumerable<Domain.Entities.Client> clients;

        // Apply filters based on query parameters
        if (!string.IsNullOrEmpty(query.GroupId))
        {
            clients = await UseCase.GetClientsByGroupAsync(query.GroupId);
        }
        else
        {
            clients = await UseCase.GetAllClientsAsync();
        }

        return Results.Ok(clients.Select(ClientResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        ClientUseCase UseCase)
    {
        var client = await UseCase.GetClientByPublicIdAsync(publicId);

        return Results.Ok(ClientResponseMapper.ToResponse(client));
    }

    private static async Task<IResult> CreateAsync(
        CreateClientRequest request,
        ClientUseCase UseCase)
    {
        var client = await UseCase.CreateClientAsync(
            request.Name,
            request.ClientGroupPublicId);

        return Results.Created(
            $"/api/clients/{client.PublicId}",
            ClientResponseMapper.ToResponse(client));
    }

    private static async Task<IResult> PatchClientAsync(
        string publicId,
        PatchClientRequest request,
        ClientUseCase UseCase)
    {
        // Update name if provided
        if (!string.IsNullOrEmpty(request.Name))
        {
            await UseCase.UpdateClientNameAsync(publicId, request.Name);
        }

        // Update group assignment if provided
        if (request.ClientGroupPublicId != null)
        {
            if (string.IsNullOrEmpty(request.ClientGroupPublicId))
            {
                // Empty string means remove from group
                await UseCase.RemoveClientFromGroupAsync(publicId);
            }
            else
            {
                // Assign to group
                await UseCase.AssignClientToGroupAsync(publicId, request.ClientGroupPublicId);
            }
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        ClientUseCase UseCase)
    {
        await UseCase.DeleteClientAsync(publicId);

        return Results.NoContent();
    }
}







