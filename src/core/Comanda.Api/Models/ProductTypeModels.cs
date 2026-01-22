namespace Comanda.Api.Models;

public record CreateProductTypeRequest(string Name);

public record ProductTypeResponse(
    string PublicId,
    string Name);







