namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/persons")
            .WithTags("Persons")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get all persons");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get person by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new person");

        group.MapPost("/{publicId}/contacts", AddContactAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Add contact to person");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", UpdateAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update person name");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a person");

        group.MapDelete("/{publicId}/contacts/{contactPublicId}", RemoveContactAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Remove contact from person");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(PersonUseCase UseCase)
    {
        var persons = await UseCase.GetAllPersonsAsync();
        return Results.Ok(persons.Select(PersonResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        PersonUseCase UseCase)
    {
        var person = await UseCase.GetPersonByPublicIdAsync(publicId);
        return Results.Ok(PersonResponseMapper.ToResponse(person));
    }

    private static async Task<IResult> CreateAsync(
        CreatePersonRequest request,
        PersonUseCase UseCase)
    {
        var person = await UseCase.CreatePersonAsync(request.Name);

        return Results.Created(
            $"/api/persons/{person.PublicId}",
            PersonResponseMapper.ToResponse(person));
    }

    private static async Task<IResult> AddContactAsync(
        string publicId,
        AddPersonContactRequest request,
        PersonUseCase UseCase)
    {
        var person = await UseCase.AddContactAsync(publicId, request.Type, request.Value);
        return Results.Ok(PersonResponseMapper.ToResponse(person));
    }

    private static async Task<IResult> UpdateAsync(
        string publicId,
        UpdatePersonRequest request,
        PersonUseCase UseCase)
    {
        await UseCase.UpdatePersonNameAsync(publicId, request.Name);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        PersonUseCase UseCase)
    {
        await UseCase.DeletePersonAsync(publicId);
        return Results.NoContent();
    }

    private static async Task<IResult> RemoveContactAsync(
        string publicId,
        string contactPublicId,
        PersonUseCase UseCase)
    {
        await UseCase.RemoveContactAsync(publicId, contactPublicId);
        return Results.NoContent();
    }
}
