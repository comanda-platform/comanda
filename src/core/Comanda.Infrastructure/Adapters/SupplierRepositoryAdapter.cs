namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;
using Comanda.Shared.Enums;

public class SupplierRepositoryAdapter(
    Database.Repositories.ISupplierRepository databaseRepository,
    Context context) : Domain.Repositories.ISupplierRepository
{
    private readonly Database.Repositories.ISupplierRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Supplier?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Supplier>> GetByTypeAsync(SupplierType type)
    {
        var entities = await _databaseRepository.GetByTypeAsync((int)type);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Supplier supplier)
    {
        var entity = supplier.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(supplier.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Supplier, supplier.PublicId);

        supplier.UpdatePersistence(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Supplier supplier)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(supplier.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Supplier, supplier.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}







