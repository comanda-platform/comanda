namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class NoteEndpoints
{
    public static void MapNoteEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/notes")
            .WithTags("Notes")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get notes with optional filters")
            .WithDescription("Retrieves notes. Use query parameters to filter: clientId, clientGroupId, locationId, orderId, orderLineId, productId, sideId");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get note by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new note")
            .WithDescription("Create a note for a specific entity (client, client group, location, order, order line, product, or side)");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a note");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] NoteQueryParameters query,
        NoteUseCase UseCase)
    {
        IEnumerable<Domain.Entities.Note> notes;

        // Apply filters based on query parameters
        if (!string.IsNullOrEmpty(query.ClientId))
        {
            notes = await UseCase.GetByClientAsync(query.ClientId);
        }
        else if (!string.IsNullOrEmpty(query.ClientGroupId))
        {
            notes = await UseCase.GetByClientGroupAsync(query.ClientGroupId);
        }
        else if (!string.IsNullOrEmpty(query.LocationId))
        {
            notes = await UseCase.GetByLocationAsync(query.LocationId);
        }
        else if (!string.IsNullOrEmpty(query.OrderId))
        {
            notes = await UseCase.GetByOrderAsync(query.OrderId);
        }
        else if (!string.IsNullOrEmpty(query.OrderLineId))
        {
            notes = await UseCase.GetByOrderLineAsync(query.OrderLineId);
        }
        else if (!string.IsNullOrEmpty(query.ProductId))
        {
            notes = await UseCase.GetByProductAsync(query.ProductId);
        }
        else if (!string.IsNullOrEmpty(query.SideId))
        {
            notes = await UseCase.GetBySideAsync(query.SideId);
        }
        else
        {
            // Return empty if no valid filter
            notes = Array.Empty<Domain.Entities.Note>();
        }

        return Results.Ok(notes.Select(NoteResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        NoteUseCase UseCase)
    {
        var note = await UseCase.GetByPublicIdAsync(publicId);

        return Results.Ok(NoteResponseMapper.ToResponse(note));
    }

    private static async Task<IResult> CreateAsync(CreateNoteRequest request, NoteUseCase UseCase)
    {
        Domain.Entities.Note note;

        // Create note based on which entity ID is provided
        if (!string.IsNullOrEmpty(request.ClientPublicId))
        {
            note = await UseCase.CreateForClientAsync(request.Content, request.ClientPublicId);
        }
        else if (!string.IsNullOrEmpty(request.ClientGroupPublicId))
        {
            note = await UseCase.CreateForClientGroupAsync(request.Content, request.ClientGroupPublicId);
        }
        else if (!string.IsNullOrEmpty(request.LocationPublicId))
        {
            note = await UseCase.CreateForLocationAsync(request.Content, request.LocationPublicId);
        }
        else if (!string.IsNullOrEmpty(request.OrderPublicId))
        {
            note = await UseCase.CreateForOrderAsync(request.Content, request.OrderPublicId);
        }
        else if (!string.IsNullOrEmpty(request.OrderLinePublicId))
        {
            note = await UseCase.CreateForOrderLineAsync(request.Content, request.OrderLinePublicId);
        }
        else if (!string.IsNullOrEmpty(request.ProductPublicId))
        {
            note = await UseCase.CreateForProductAsync(request.Content, request.ProductPublicId);
        }
        else if (!string.IsNullOrEmpty(request.SidePublicId))
        {
            note = await UseCase.CreateForSideAsync(request.Content, request.SidePublicId);
        }
        else
        {
            return Results.BadRequest("Must provide an entity ID (client, clientGroup, location, order, orderLine, product, or side)");
        }

        return Results.Created($"/api/notes/{note.PublicId}", NoteResponseMapper.ToResponse(note));
    }

    private static async Task<IResult> DeleteAsync(string publicId, NoteUseCase UseCase)
    {
        await UseCase.DeleteAsync(publicId);
        return Results.NoContent();
    }
}







