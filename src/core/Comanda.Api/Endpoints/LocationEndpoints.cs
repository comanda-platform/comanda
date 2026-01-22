namespace Comanda.Api.Endpoints;

using Comanda.Api.Filters;
using Comanda.Api.Mappers;
using Comanda.Api.Models;
using Comanda.Application.UseCases;
using Comanda.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

public static class LocationEndpoints
{
    public static void MapLocationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/locations")
            .WithTags("Locations")
            .RequireAuthorization();

        #region GET
        group.MapGet("/", GetAllAsync)
            .WithSummary("Get locations with optional filters")
            .WithDescription("Retrieves locations. Use query parameters to filter: active, company, deliveryDestinations, type, latitude/longitude/radiusKm for nearby search");

        group.MapGet("/{publicId}", GetByPublicIdAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Get location by ID");

        group.MapGet("/{publicId}/distance", CalculateDistanceAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Calculate distance from location to target coordinates");
        #endregion

        #region POST
        group.MapPost("/", CreateAsync)
            .WithSummary("Create a new location");
        #endregion

        #region PATCH
        group.MapPatch("/{publicId}", PatchLocationAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Update location fields or status")
            .WithDescription("Update location details, type, activate/deactivate, or manage client/group assignments");
        #endregion

        #region DELETE
        group.MapDelete("/{publicId}", DeleteAsync)
            .AddEndpointFilter<RequirePublicIdFilter>()
            .WithSummary("Delete a location");
        #endregion
    }

    private static async Task<IResult> GetAllAsync(
        [AsParameters] LocationQueryParameters query,
        LocationUseCase UseCase)
    {
        IEnumerable<Domain.Entities.Location> locations;

        // Apply filters based on query parameters
        if (query.Active.HasValue && query.Active.Value)
        {
            locations = await UseCase.GetActiveLocationsAsync();
        }
        else if (query.Company.HasValue && query.Company.Value)
        {
            locations = await UseCase.GetCompanyLocationsAsync();
        }
        else if (query.DeliveryDestinations.HasValue && query.DeliveryDestinations.Value)
        {
            locations = await UseCase.GetDeliveryDestinationsAsync();
        }
        else if (query.Type.HasValue)
        {
            locations = await UseCase.GetLocationsByTypeAsync(query.Type.Value);
        }
        else if (query.Latitude.HasValue && query.Longitude.HasValue && query.RadiusKm.HasValue)
        {
            locations = await UseCase.GetNearbyLocationsAsync(
                query.Latitude.Value,
                query.Longitude.Value,
                query.RadiusKm.Value);
        }
        else
        {
            locations = await UseCase.GetAllLocationsAsync();
        }

        return Results.Ok(locations.Select(LocationResponseMapper.ToResponse));
    }

    private static async Task<IResult> GetByPublicIdAsync(
        string publicId,
        LocationUseCase UseCase)
    {
        var location = await UseCase.GetLocationByPublicIdAsync(publicId);

        return Results.Ok(LocationResponseMapper.ToResponse(location));
    }

    private static async Task<IResult> CreateAsync(
        CreateLocationRequest request,
        LocationUseCase UseCase)
    {
        var location = await UseCase.CreateLocationAsync(
            request.Type,
            request.Name,
            request.Latitude,
            request.Longitude,
            request.AddressLine,
            request.ClientPublicId,
            request.ClientGroupPublicId);

        return Results.Created(
            $"/api/locations/{location.PublicId}",
            LocationResponseMapper.ToResponse(location));
    }

    private static async Task<IResult> PatchLocationAsync(
        string publicId,
        PatchLocationRequest request,
        LocationUseCase UseCase)
    {
        // Update basic details if any are provided
        if (!string.IsNullOrEmpty(request.Name) ||
            request.Latitude.HasValue ||
            request.Longitude.HasValue ||
            !string.IsNullOrEmpty(request.AddressLine))
        {
            await UseCase.UpdateLocationDetailsAsync(
                publicId,
                request.Name,
                request.Latitude,
                request.Longitude,
                request.AddressLine);
        }

        // Update type if provided
        if (request.Type.HasValue)
        {
            await UseCase.UpdateLocationTypeAsync(publicId, request.Type.Value);
        }

        // Update active status if provided
        if (request.IsActive.HasValue)
        {
            if (request.IsActive.Value)
            {
                await UseCase.ActivateLocationAsync(publicId);
            }
            else
            {
                await UseCase.DeactivateLocationAsync(publicId);
            }
        }

        // Assign to client if provided
        if (!string.IsNullOrEmpty(request.ClientPublicId))
        {
            await UseCase.AssignLocationToClientAsync(publicId, request.ClientPublicId);
        }

        // Assign to client group if provided
        if (!string.IsNullOrEmpty(request.ClientGroupPublicId))
        {
            await UseCase.AssignLocationToClientGroupAsync(publicId, request.ClientGroupPublicId);
        }

        // Unassign from client if requested
        if (request.UnassignClient.HasValue && request.UnassignClient.Value)
        {
            await UseCase.UnassignLocationFromClientAsync(publicId);
        }

        // Unassign from client group if requested
        if (request.UnassignClientGroup.HasValue && request.UnassignClientGroup.Value)
        {
            await UseCase.UnassignLocationFromClientGroupAsync(publicId);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(
        string publicId,
        LocationUseCase UseCase)
    {
        await UseCase.DeleteLocationAsync(publicId);

        return Results.NoContent();
    }

    private static async Task<IResult> CalculateDistanceAsync(
        string publicId,
        [AsParameters] CalculateDistanceRequest request,
        LocationUseCase UseCase)
    {
        var distance = await UseCase.CalculateDistanceAsync(
            publicId,
            request.TargetLatitude,
            request.TargetLongitude);

        return Results.Ok(new DistanceResponse(distance));
    }
}







