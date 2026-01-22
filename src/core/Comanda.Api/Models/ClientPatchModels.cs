namespace Comanda.Api.Models;

/// <summary>
/// Request model for patching a client (unified field updates)
/// </summary>
public record PatchClientRequest
{
    public string? Name { get; init; }
    public string? ClientGroupPublicId { get; init; }
}

/// <summary>
/// Query parameters for filtering clients
/// </summary>
public record ClientQueryParameters
{
    public string? GroupId { get; init; }
}







