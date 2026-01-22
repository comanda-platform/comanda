namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface INoteRepository
{
    Task<Note?> GetByIdAsync(int id);
    Task<Note?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Note>> GetByClientPublicIdAsync(string clientPublicId);
    Task<IEnumerable<Note>> GetByClientGroupPublicIdAsync(string clientGroupPublicId);
    Task<IEnumerable<Note>> GetByLocationPublicIdAsync(string locationPublicId);
    Task<IEnumerable<Note>> GetByOrderPublicIdAsync(string orderPublicId);
    Task<IEnumerable<Note>> GetByOrderLinePublicIdAsync(string orderLinePublicId);
    Task<IEnumerable<Note>> GetByProductPublicIdAsync(string productPublicId);
    Task<IEnumerable<Note>> GetBySidePublicIdAsync(string sidePublicId);
    Task AddAsync(Note note);
    Task DeleteAsync(Note note);
}







