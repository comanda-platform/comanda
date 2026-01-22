namespace Comanda.Domain.StateMachines;

using Comanda.Shared.Enums;

public static class LocationStateMachine
{
    private static readonly Dictionary<LocationType, string> TypeDescriptions = new()
    {
        [LocationType.ClientHome] = "Client's home address",
        [LocationType.Business] = "Business/commercial location",
        [LocationType.OurRestaurant] = "Our restaurant location",
        [LocationType.OurKitchen] = "Our kitchen/preparation facility",
        [LocationType.OurWarehouse] = "Our warehouse/storage facility"
    };

    private static readonly Dictionary<LocationType, bool> RequiresClientAssignment = new()
    {
        [LocationType.ClientHome] = true,
        [LocationType.Business] = true,
        [LocationType.OurRestaurant] = false,
        [LocationType.OurKitchen] = false,
        [LocationType.OurWarehouse] = false
    };

    private static readonly Dictionary<LocationType, bool> CanBeDeliveryDestination = new()
    {
        [LocationType.ClientHome] = true,
        [LocationType.Business] = true,
        [LocationType.OurRestaurant] = false,
        [LocationType.OurKitchen] = false,
        [LocationType.OurWarehouse] = false
    };

    public static string GetDescription(LocationType type)
    {
        return TypeDescriptions.TryGetValue(type, out var description)
            ? description
            : "Unknown location type";
    }

    public static bool MustBeAssignedToClient(LocationType type)
    {
        return RequiresClientAssignment.TryGetValue(type, out var required) && required;
    }

    public static bool CanBeDeliveryTarget(LocationType type)
    {
        return CanBeDeliveryDestination.TryGetValue(type, out var canDeliver) && canDeliver;
    }

    public static bool IsCompanyOwned(LocationType type)
    {
        return type == LocationType.OurRestaurant ||
               type == LocationType.OurKitchen ||
               type == LocationType.OurWarehouse;
    }

    public static IEnumerable<LocationType> GetClientLocationTypes()
    {
        return new[] { LocationType.ClientHome, LocationType.Business };
    }

    public static IEnumerable<LocationType> GetCompanyLocationTypes()
    {
        return [LocationType.OurRestaurant, LocationType.OurKitchen, LocationType.OurWarehouse];
    }

    public static bool CanTransitionTo(LocationType from, LocationType to)
    {
        // Company locations can transition between themselves
        if (IsCompanyOwned(from) && IsCompanyOwned(to))
            return true;

        // Client locations can transition between themselves
        if (!IsCompanyOwned(from) && !IsCompanyOwned(to))
            return true;

        // Cannot transition between company and client locations
        return false;
    }
}






