namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class AuthorizationEndpoints
{
    public static void MapAuthorizationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/authorizations")
            .WithTags("Authorizations")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get authorizations with optional filters")
            .WithDescription("Retrieves authorizations. Use query parameters to filter: personPublicId, accountPublicId, activeOnly");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get authorization by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new authorization");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchAuthorizationAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update authorization")
            .WithDescription("Update authorization role or active status");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete an authorization");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] AuthorizationQueryParameters query,
        AuthorizationUseCase authorizationUseCase,
        PersonUseCase personUseCase,
        AccountUseCase accountUseCase)
    {
        IEnumerable<Domain.Entities.Authorization> authorizations;

        // Apply filters based on query parameters
        if (!string.IsNullOrEmpty(query.PersonPublicId) && query.ActiveOnly == true)
        {
            authorizations = await authorizationUseCase.GetActiveAuthorizationsByPersonPublicIdAsync(query.PersonPublicId);
        }
        else if (!string.IsNullOrEmpty(query.PersonPublicId))
        {
            authorizations = await authorizationUseCase.GetAuthorizationsByPersonPublicIdAsync(query.PersonPublicId);
        }
        else if (!string.IsNullOrEmpty(query.AccountPublicId) && query.ActiveOnly == true)
        {
            authorizations = await authorizationUseCase.GetActiveAuthorizationsByAccountPublicIdAsync(query.AccountPublicId);
        }
        else if (!string.IsNullOrEmpty(query.AccountPublicId))
        {
            authorizations = await authorizationUseCase.GetAuthorizationsByAccountPublicIdAsync(query.AccountPublicId);
        }
        else
        {
            authorizations = await authorizationUseCase.GetAllAuthorizationsAsync();
        }

        // Fetch person and account names for each authorization
        var responses = new List<AuthorizationResponse>();
        foreach (var auth in authorizations)
        {
            var person = await personUseCase.GetPersonByPublicIdAsync(auth.PersonPublicId);
            var account = await accountUseCase.GetAccountByPublicIdAsync(auth.AccountPublicId);

            responses.Add(AuthorizationResponseMapper.ToResponse(auth, person.Name, account.Name));
        }

        return Results.Ok(responses);
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        AuthorizationUseCase authorizationUseCase,
        PersonUseCase personUseCase,
        AccountUseCase accountUseCase)
    {
        var authorization = await authorizationUseCase.GetAuthorizationByPublicIdAsync(publicId);

        var person = await personUseCase.GetPersonByPublicIdAsync(authorization.PersonPublicId);
        var account = await accountUseCase.GetAccountByPublicIdAsync(authorization.AccountPublicId);

        return Results.Ok(AuthorizationResponseMapper.ToResponse(authorization, person.Name, account.Name));
    }

    private static async Task<IResult> CreateAsync(
        CreateAuthorizationRequest request,
        AuthorizationUseCase authorizationUseCase,
        PersonUseCase personUseCase,
        AccountUseCase accountUseCase)
    {
        var authorization = await authorizationUseCase.CreateAuthorizationAsync(
            request.PersonPublicId,
            request.AccountPublicId,
            request.Role);

        var person = await personUseCase.GetPersonByPublicIdAsync(authorization.PersonPublicId);
        var account = await accountUseCase.GetAccountByPublicIdAsync(authorization.AccountPublicId);

        return Results.Created(
            $"/api/authorizations/{authorization.PublicId}",
            AuthorizationResponseMapper.ToResponse(authorization, person.Name, account.Name));
    }

    private static async Task<IResult> PatchAuthorizationAsync(
        string publicId,
        PatchAuthorizationRequest request,
        AuthorizationUseCase UseCase)
    {
        // Update role if provided
        if (request.Role.HasValue)
        {
            await UseCase.UpdateAuthorizationRoleAsync(publicId, request.Role.Value);
        }

        // Update active status if provided
        if (request.IsActive.HasValue)
        {
            if (request.IsActive.Value)
            {
                await UseCase.ActivateAuthorizationAsync(publicId);
            }
            else
            {
                await UseCase.DeactivateAuthorizationAsync(publicId);
            }
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        AuthorizationUseCase UseCase)
    {
        await UseCase.DeleteAuthorizationAsync(publicId);
        return Results.NoContent();
    }
}
