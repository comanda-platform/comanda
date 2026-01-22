namespace Comanda.Domain;

public class NotFoundException : Exception {
    
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(
        string entityTypePrintName,
        string publicId) : base($"{entityTypePrintName} '{publicId}' not found.") { }
}

public sealed class ConflictException(string message) : Exception(message) { }







