using Comanda.Domain.Entities;

namespace Comanda.Domain.Repositories;

public interface IAuthorizationRepository
{
    Task<Authorization?> GetByIdAsync(int id);
    Task<Authorization?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Authorization>> GetAllAsync();
    Task<IEnumerable<Authorization>> GetByPersonPublicIdAsync(string personPublicId);
    Task<IEnumerable<Authorization>> GetByAccountPublicIdAsync(string accountPublicId);
    Task<IEnumerable<Authorization>> GetActiveByPersonPublicIdAsync(string personPublicId);
    Task<IEnumerable<Authorization>> GetActiveByAccountPublicIdAsync(string accountPublicId);
    Task AddAsync(Authorization authorization);
    Task UpdateAsync(Authorization authorization);
    Task DeleteAsync(Authorization authorization);
}
