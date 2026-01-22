namespace Comanda.Shared.Enums;

/// <summary>
/// Represents the status of a production batch in the kitchen
/// </summary>
public enum BatchStatus
{
    /// <summary>
    /// Cooking has started but not yet completed
    /// </summary>
    InProgress = 0,

    /// <summary>
    /// Batch cooking is complete and yield has been recorded
    /// </summary>
    Completed = 1
}







