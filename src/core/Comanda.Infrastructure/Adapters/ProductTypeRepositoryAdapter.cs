namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class ProductTypeRepositoryAdapter(
    Database.Repositories.IProductTypeRepository databaseRepository,
    Context context) : Domain.Repositories.IProductTypeRepository
{
    private readonly Database.Repositories.IProductTypeRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<ProductType?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);
        return entity?.FromPersistence();
    }

    public async Task<ProductType?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);
        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<ProductType>> GetAllAsync()
    {
        var entities = await _databaseRepository.Query().ToListAsync();
        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(ProductType productType)
    {
        var entity = productType.ToPersistence();
        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(ProductType productType)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(productType.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ProductType, productType.PublicId);

        entity.Name = productType.Name;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ProductType productType)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(productType.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ProductType, productType.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}







