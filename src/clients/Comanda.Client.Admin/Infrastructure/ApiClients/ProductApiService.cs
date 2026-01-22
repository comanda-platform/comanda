using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class ProductApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    // Products
    public async Task<IEnumerable<ProductResponse>> GetProductsAsync(
        string? productTypeId = null,
        string? searchTerm = null)
    {
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(productTypeId))
            queryParams.Add($"productTypeId={productTypeId}");
        if (!string.IsNullOrEmpty(searchTerm))
            queryParams.Add($"searchTerm={searchTerm}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

        return await GetAsync<IEnumerable<ProductResponse>>($"api/products{query}") ?? [];
    }

    public async Task<ProductResponse?> GetProductByIdAsync(string publicId)
    {
        return await GetAsync<ProductResponse>($"api/products/{publicId}");
    }

    public async Task<ProductResponse?> CreateProductAsync(CreateProductRequest request)
    {
        var response = await PostAsync("api/products", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ProductResponse>();
    }

    public async Task<bool> UpdateProductAsync(string publicId, UpdateProductRequest request)
    {
        var response = await PatchAsync($"api/products/{publicId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProductPriceAsync(string publicId, decimal newPrice)
    {
        var request = new UpdateProductPriceRequest(newPrice);
        var response = await PostAsync($"api/products/{publicId}/update-price", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProductAsync(string publicId)
    {
        var response = await DeleteAsync($"api/products/{publicId}");
        return response.IsSuccessStatusCode;
    }

    // Product Types
    public async Task<IEnumerable<ProductTypeResponse>> GetProductTypesAsync()
    {
        return await GetAsync<IEnumerable<ProductTypeResponse>>("api/product-types") ?? [];
    }

    public async Task<ProductTypeResponse?> GetProductTypeByIdAsync(string publicId)
    {
        return await GetAsync<ProductTypeResponse>($"api/product-types/{publicId}");
    }

    public async Task<ProductTypeResponse?> CreateProductTypeAsync(CreateProductTypeRequest request)
    {
        var response = await PostAsync("api/product-types", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ProductTypeResponse>();
    }

    public async Task<bool> DeleteProductTypeAsync(string publicId)
    {
        var response = await DeleteAsync($"api/product-types/{publicId}");
        return response.IsSuccessStatusCode;
    }
}










