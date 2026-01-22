namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface ISideRepository
{
    Task<Side?> GetByIdAsync(int id);
    Task<Side?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Side>> GetAllAsync();
    Task<IEnumerable<Side>> GetActiveAsync();
    Task AddAsync(Side side);
    Task UpdateAsync(Side side);
    Task DeleteAsync(Side side);
}







