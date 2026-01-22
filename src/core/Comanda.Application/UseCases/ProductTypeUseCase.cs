namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class ProductTypeUseCase(IProductTypeRepository repository) : UseCaseBase(EntityTypePrintNames.ProductType)
{
    private readonly IProductTypeRepository _repository = repository;

    public async Task<ProductType> CreateProductTypeAsync(string name)
    {
        var productType = new ProductType(name);

        await _repository.AddAsync(productType);

        return productType;
    }

    public async Task<ProductType> GetByPublicIdAsync(string publicId) 
        => await _repository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<IEnumerable<ProductType>> GetAllAsync()
        => await _repository.GetAllAsync();
}






