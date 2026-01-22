using Comanda.Domain.Entities;

namespace Comanda.Domain.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(int id);
    Task<Person?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Person>> GetAllAsync();
    Task AddAsync(Person person);
    Task UpdateAsync(Person person);
    Task DeleteAsync(Person person);
}
