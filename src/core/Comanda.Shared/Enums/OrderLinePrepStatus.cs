namespace Comanda.Shared.Enums;

/// <summary>
/// Represents the preparation status of an order line in the kitchen
/// </summary>
public enum OrderLinePrepStatus
{
    /// <summary>
    /// Line has not been started yet
    /// </summary>
    Pending = 0,
    
    /// <summary>
    /// Kitchen staff has started preparing this line
    /// </summary>
    InProgress = 1,
    
    /// <summary>
    /// Line has been plated and is ready for the order
    /// </summary>
    Plated = 2,
    
    /// <summary>
    /// Line has been completed (order delivered/picked up)
    /// </summary>
    Completed = 3
}







