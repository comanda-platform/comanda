namespace Comanda.Application.UseCases;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Domain.Repositories;
using Comanda.Domain;

public class SupplierUseCase(ISupplierRepository supplierRepository) : UseCaseBase(EntityTypePrintNames.Supplier)
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;

    public async Task<Supplier> CreateAsync(
        string name,
        SupplierType type)
    {
        var supplier = new Supplier(name, type);

        await _supplierRepository.AddAsync(supplier);

        return supplier;
    }

    public async Task<Supplier?> GetByPublicIdAsync(string publicId)
        => await _supplierRepository.GetByPublicIdAsync(publicId);

    public async Task<IEnumerable<Supplier>> GetAllAsync()
        => await _supplierRepository.GetAllAsync();

    public async Task<IEnumerable<Supplier>> GetByTypeAsync(SupplierType type)
        => await _supplierRepository.GetByTypeAsync(type);

    public async Task UpdateNameAsync(string publicId, string name)
    {
        var supplier = await _supplierRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        supplier.UpdateName(name);

        await _supplierRepository.UpdateAsync(supplier);
    }

    public async Task UpdateTypeAsync(string publicId, SupplierType type)
    {
        var supplier = await _supplierRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        supplier.UpdateType(type);

        await _supplierRepository.UpdateAsync(supplier);
    }

    public async Task DeleteAsync(string publicId)
    {
        var supplier = await _supplierRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _supplierRepository.DeleteAsync(supplier);
    }
}







