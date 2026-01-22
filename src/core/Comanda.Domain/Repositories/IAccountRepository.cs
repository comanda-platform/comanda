using Comanda.Domain.Entities;

namespace Comanda.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(int id);
    Task<Account?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Account>> GetAllAsync();
    Task<IEnumerable<Account>> GetWithCreditLineAsync();
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Account account);
}
