namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(int id);
    Task<Client?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Client>> GetAllAsync();
    Task<IEnumerable<Client>> GetByGroupIdAsync(int clientGroupId);
    Task<IEnumerable<Client>> GetByGroupPublicIdAsync(string clientGroupPublicId);
    Task AddAsync(Client client);
    Task UpdateAsync(Client client);
    Task DeleteAsync(Client client);
}







