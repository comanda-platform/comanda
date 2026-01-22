namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class ProductUseCase(
    IProductRepository productRepository,
    IProductTypeRepository productTypeRepository) : UseCaseBase(EntityTypePrintNames.Product)
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductTypeRepository _productTypeRepository = productTypeRepository;

    public async Task<Product> CreateProductAsync(
        string name,
        decimal initialPrice,
        string productTypePublicId,
        string? description = null)
    {
        var productType = await _productTypeRepository.GetByPublicIdAsync(productTypePublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ProductType, productTypePublicId);

        var product = new Product(name, initialPrice, productType, description);

        await _productRepository.AddAsync(product);

        return product;
    }

    public async Task<Product> GetProductByPublicIdAsync(string publicId)
        => await _productRepository.GetByPublicIdAsync(publicId);

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
        => await _productRepository.GetAllAsync();

    public async Task<IEnumerable<Product>> GetProductsByTypeAsync(string productTypePublicId)
    {
        var productType = await _productTypeRepository.GetByPublicIdAsync(productTypePublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ProductType, productTypePublicId);

        return await _productRepository.GetByTypeAsync(productType);
    }

    public async Task UpdateProductAsync(
        string publicId,
        string name,
        string? description)
    {
        var product = await _productRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        product.UpdateDetails(
            name,
            description);

        await _productRepository.UpdateAsync(product);
    }

    public async Task UpdateProductPriceAsync(
        string publicId,
        decimal newPrice)
    {
        var product = await _productRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        product.UpdatePrice(newPrice);

        await _productRepository.UpdateAsync(product);
    }

    public async Task<decimal> GetProductPriceOnDateAsync(
        string publicId,
        DateTime date)
    {
        var product = await _productRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        return product.GetPriceOn(date);
    }

    public async Task DeleteProductAsync(string publicId)
    {
        var product = await _productRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _productRepository.DeleteAsync(product);
    }
}







