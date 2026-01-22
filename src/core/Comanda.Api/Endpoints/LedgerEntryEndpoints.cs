namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

public static class LedgerEntryEndpoints
{
    public static void MapLedgerEntryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/ledger-entries")
            .WithTags("Ledger Entries")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get ledger entries with optional filters")
            .WithDescription("Retrieves ledger entries. Use query parameters to filter: clientId, from/to");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get ledger entry by ID");

        group.MapGet("/balance", GetClientBalanceAsync)
            .WithSummary("Get client balance")
            .WithDescription("Get balance for a specific client (requires clientId query parameter)");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new ledger entry")
            .WithDescription("Create a credit, payment, adjustment, or write-off entry");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] LedgerEntryQueryParameters query,
        LedgerEntryUseCase UseCase)
    {
        // Apply filters based on query parameters
        if (!string.IsNullOrEmpty(query.ClientId) && query.From.HasValue && query.To.HasValue)
        {
            var entries = await UseCase.GetEntriesByClientAndDateRangeAsync(
                query.ClientId,
                query.From.Value,
                query.To.Value);
            return Results.Ok(entries.Select(LedgerEntryResponseMapper.ToResponse));
        }
        else if (!string.IsNullOrEmpty(query.ClientId))
        {
            var entries = await UseCase.GetEntriesByClientAsync(query.ClientId);
            return Results.Ok(entries.Select(LedgerEntryResponseMapper.ToResponse));
        }
        else
        {
            // Return empty if no valid filter
            return Results.Ok(Array.Empty<object>());
        }
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        LedgerEntryUseCase UseCase)
    {
        var entry = await UseCase.GetEntryByPublicIdAsync(publicId);

        return Results.Ok(LedgerEntryResponseMapper.ToResponse(entry));
    }

    private static async Task<IResult> GetClientBalanceAsync(
        [AsParameters] LedgerEntryQueryParameters query,
        LedgerEntryUseCase UseCase)
    {
        if (string.IsNullOrEmpty(query.ClientId))
        {
            return Results.BadRequest("clientId is required");
        }

        var balance = await UseCase.GetClientBalanceAsync(query.ClientId);

        return Results.Ok(new { balance });
    }

    private static async Task<IResult> CreateAsync(
        CreateLedgerEntryRequest request,
        LedgerEntryUseCase UseCase)
    {
        Domain.Entities.LedgerEntry entry;

        // Create entry based on type
        switch (request.EntryType?.ToLowerInvariant())
        {
            case "credit":
                entry = await UseCase.CreateCreditAsync(
                    request.ClientPublicId,
                    request.Amount,
                    request.OrderLinePublicId);
                break;

            case "payment":
                if (string.IsNullOrEmpty(request.PaymentMethod))
                {
                    return Results.BadRequest("PaymentMethod is required for payment entries");
                }
                // Parse PaymentMethod string to enum
                if (!Enum.TryParse<Comanda.Shared.Enums.PaymentMethod>(request.PaymentMethod, true, out var paymentMethod))
                {
                    return Results.BadRequest("Invalid payment method");
                }
                entry = await UseCase.CreatePaymentAsync(
                    request.ClientPublicId,
                    request.Amount,
                    paymentMethod);
                break;

            case "adjustment":
                entry = await UseCase.CreateAdjustmentAsync(
                    request.ClientPublicId,
                    request.Amount);
                break;

            case "writeoff":
            case "write-off":
                entry = await UseCase.CreateWriteOffAsync(
                    request.ClientPublicId,
                    request.Amount);
                break;

            default:
                return Results.BadRequest("Invalid entry type. Must be: credit, payment, adjustment, or writeoff");
        }

        return Results.Created(
            $"/api/ledger-entries/{entry.PublicId}",
            LedgerEntryResponseMapper.ToResponse(entry));
    }
}







