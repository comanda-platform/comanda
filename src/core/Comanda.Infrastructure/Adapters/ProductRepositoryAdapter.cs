namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class ProductRepositoryAdapter(
    Database.Repositories.IProductRepository databaseRepository,
    Context context) : Domain.Repositories.IProductRepository
{
    private readonly Database.Repositories.IProductRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Product?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Product?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Product>> GetByTypeAsync(ProductType type)
    {
        var typeEntity = type.ToPersistence();

        var entities = await _databaseRepository.Query()
            .Where(p => p.Type.Id == typeEntity.Id)
            .ToListAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Product product)
    {
        var entity = product.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Product product)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(product.PublicId) 
            ?? throw new NotFoundException(EntityTypePrintNames.Product, product.PublicId);

        product.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(product.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Product, product.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}






