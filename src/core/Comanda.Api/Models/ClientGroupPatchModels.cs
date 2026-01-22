namespace Comanda.Api.Models;

/// <summary>
/// Request model for patching a client group (unified field updates)
/// </summary>
public record PatchClientGroupRequest
{
    public string? Name { get; init; }
    public bool? HasCreditLine { get; init; }
}

/// <summary>
/// Query parameters for filtering client groups
/// </summary>
public record ClientGroupQueryParameters
{
    public bool? WithCreditLine { get; init; }
}







