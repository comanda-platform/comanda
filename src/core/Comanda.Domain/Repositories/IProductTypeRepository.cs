namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface IProductTypeRepository
{
    Task<ProductType?> GetByIdAsync(int id);
    Task<ProductType?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<ProductType>> GetAllAsync();
    Task AddAsync(ProductType productType);
    Task UpdateAsync(ProductType productType);
    Task DeleteAsync(ProductType productType);
}






