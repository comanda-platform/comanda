namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface IClientGroupRepository
{
    Task<ClientGroup?> GetByIdAsync(int id);
    Task<ClientGroup?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<ClientGroup>> GetAllAsync();
    Task<IEnumerable<ClientGroup>> GetWithCreditLineAsync();
    Task AddAsync(ClientGroup clientGroup);
    Task UpdateAsync(ClientGroup clientGroup);
    Task DeleteAsync(ClientGroup clientGroup);
}







