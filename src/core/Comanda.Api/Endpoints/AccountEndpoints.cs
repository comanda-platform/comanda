namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/accounts")
            .WithTags("Accounts")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get accounts with optional filters")
            .WithDescription("Retrieves accounts. Use query parameters to filter: withCreditLine");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get account by ID");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new account");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchAccountAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update account fields")
            .WithDescription("Update account name, credit line status, or credit limit");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete an account");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] AccountQueryParameters query,
        AccountUseCase UseCase)
    {
        IEnumerable<Domain.Entities.Account> accounts;

        // Apply filters based on query parameters
        if (query.WithCreditLine == true)
        {
            accounts = await UseCase.GetAccountsWithCreditLineAsync();
        }
        else
        {
            accounts = await UseCase.GetAllAccountsAsync();
        }

        return Results.Ok(accounts.Select(AccountResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        AccountUseCase UseCase)
    {
        var account = await UseCase.GetAccountByPublicIdAsync(publicId);
        return Results.Ok(AccountResponseMapper.ToResponse(account));
    }

    private static async Task<IResult> CreateAsync(
        CreateAccountRequest request,
        AccountUseCase UseCase)
    {
        var account = await UseCase.CreateAccountAsync(
            request.Name,
            request.HasCreditLine,
            request.CreditLimit);

        return Results.Created(
            $"/api/accounts/{account.PublicId}",
            AccountResponseMapper.ToResponse(account));
    }

    private static async Task<IResult> PatchAccountAsync(
        string publicId,
        PatchAccountRequest request,
        AccountUseCase UseCase)
    {
        // Update name if provided
        if (!string.IsNullOrEmpty(request.Name))
        {
            await UseCase.UpdateAccountNameAsync(publicId, request.Name);
        }

        // Update credit line if provided
        if (request.HasCreditLine.HasValue)
        {
            if (request.HasCreditLine.Value)
            {
                await UseCase.EnableCreditLineAsync(publicId, request.CreditLimit);
            }
            else
            {
                await UseCase.DisableCreditLineAsync(publicId);
            }
        }
        // Update credit limit alone if provided (and HasCreditLine not specified)
        else if (request.CreditLimit.HasValue)
        {
            await UseCase.UpdateCreditLimitAsync(publicId, request.CreditLimit);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        AccountUseCase UseCase)
    {
        await UseCase.DeleteAccountAsync(publicId);
        return Results.NoContent();
    }
}
