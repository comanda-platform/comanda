namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class NoteUseCase(INoteRepository noteRepository) : UseCaseBase(EntityTypePrintNames.Note)
{
    private readonly INoteRepository _noteRepository = noteRepository;

    public async Task<Note> CreateForClientAsync(string content, string clientPublicId, string? createdByPublicId = null)
    {
        var note = Note.ForClient(content, clientPublicId, createdByPublicId);
        await _noteRepository.AddAsync(note);
        return note;
    }

    public async Task<Note> CreateForClientGroupAsync(string content, string clientGroupPublicId, string? createdByPublicId = null)
    {
        var note = Note.ForClientGroup(content, clientGroupPublicId, createdByPublicId);
        await _noteRepository.AddAsync(note);
        return note;
    }

    public async Task<Note> CreateForLocationAsync(string content, string locationPublicId, string? createdByPublicId = null)
    {
        var note = Note.ForLocation(content, locationPublicId, createdByPublicId);
        await _noteRepository.AddAsync(note);
        return note;
    }

    public async Task<Note> CreateForOrderAsync(string content, string orderPublicId, string? createdByPublicId = null)
    {
        var note = Note.ForOrder(content, orderPublicId, createdByPublicId);
        await _noteRepository.AddAsync(note);
        return note;
    }

    public async Task<Note> CreateForOrderLineAsync(string content, string orderLinePublicId, string? createdByPublicId = null)
    {
        var note = Note.ForOrderLine(content, orderLinePublicId, createdByPublicId);
        await _noteRepository.AddAsync(note);
        return note;
    }

    public async Task<Note> CreateForProductAsync(string content, string productPublicId, string? createdByPublicId = null)
    {
        var note = Note.ForProduct(content, productPublicId, createdByPublicId);
        await _noteRepository.AddAsync(note);
        return note;
    }

    public async Task<Note> CreateForSideAsync(string content, string sidePublicId, string? createdByPublicId = null)
    {
        var note = Note.ForSide(content, sidePublicId, createdByPublicId);
        await _noteRepository.AddAsync(note);
        return note;
    }

    public async Task<Note?> GetByPublicIdAsync(string publicId)
        => await _noteRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Note, publicId);

    public async Task<IEnumerable<Note>> GetByClientAsync(string clientPublicId)
        => await _noteRepository.GetByClientPublicIdAsync(clientPublicId);

    public async Task<IEnumerable<Note>> GetByClientGroupAsync(string clientGroupPublicId)
        => await _noteRepository.GetByClientGroupPublicIdAsync(clientGroupPublicId);

    public async Task<IEnumerable<Note>> GetByLocationAsync(string locationPublicId)
        => await _noteRepository.GetByLocationPublicIdAsync(locationPublicId);

    public async Task<IEnumerable<Note>> GetByOrderAsync(string orderPublicId)
        => await _noteRepository.GetByOrderPublicIdAsync(orderPublicId);

    public async Task<IEnumerable<Note>> GetByOrderLineAsync(string orderLinePublicId)
        => await _noteRepository.GetByOrderLinePublicIdAsync(orderLinePublicId);

    public async Task<IEnumerable<Note>> GetByProductAsync(string productPublicId)
        => await _noteRepository.GetByProductPublicIdAsync(productPublicId);

    public async Task<IEnumerable<Note>> GetBySideAsync(string sidePublicId)
        => await _noteRepository.GetBySidePublicIdAsync(sidePublicId);

    public async Task DeleteAsync(string publicId)
    {
        var note = await _noteRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException($"Note '{publicId}' not found");

        await _noteRepository.DeleteAsync(note);
    }
}







