namespace Comanda.Api.Models;

public record CreateNoteForClientRequest(string Content, string ClientPublicId);
public record CreateNoteForClientGroupRequest(string Content, string ClientGroupPublicId);
public record CreateNoteForLocationRequest(string Content, string LocationPublicId);
public record CreateNoteForOrderRequest(string Content, string OrderPublicId);
public record CreateNoteForOrderLineRequest(string Content, string OrderLinePublicId);
public record CreateNoteForProductRequest(string Content, string ProductPublicId);
public record CreateNoteForSideRequest(string Content, string SidePublicId);

/// <summary>
/// Unified request model for creating a note (provide one entity ID)
/// </summary>
public record CreateNoteRequest
{
    public string Content { get; init; } = string.Empty;
    public string? ClientPublicId { get; init; }
    public string? ClientGroupPublicId { get; init; }
    public string? LocationPublicId { get; init; }
    public string? OrderPublicId { get; init; }
    public string? OrderLinePublicId { get; init; }
    public string? ProductPublicId { get; init; }
    public string? SidePublicId { get; init; }
}

public record NoteResponse(
    string PublicId,
    string Content,
    string ClientPublicId,
    string ClientGroupPublicId,
    string LocationPublicId,
    string OrderPublicId,
    string OrderLinePublicId,
    string ProductPublicId,
    string SidePublicId,
    string CreatedByPublicId,
    DateTime CreatedAt);







