namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Product>> GetByTypeAsync(ProductType type);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}






