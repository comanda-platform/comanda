using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class ClientApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    public async Task<IEnumerable<ClientResponse>> GetClientsAsync(
        string? clientGroupId = null,
        string? searchTerm = null)
    {
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(clientGroupId))
            queryParams.Add($"clientGroupId={clientGroupId}");
        if (!string.IsNullOrEmpty(searchTerm))
            queryParams.Add($"searchTerm={searchTerm}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

        return await GetAsync<IEnumerable<ClientResponse>>($"api/clients{query}") ?? [];
    }

    public async Task<ClientResponse?> GetClientByIdAsync(string publicId)
    {
        return await GetAsync<ClientResponse>($"api/clients/{publicId}");
    }

    public async Task<ClientResponse?> CreateClientAsync(CreateClientRequest request)
    {
        var response = await PostAsync("api/clients", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ClientResponse>();
    }

    public async Task<bool> UpdateClientAsync(string publicId, UpdateClientRequest request)
    {
        var response = await PatchAsync($"api/clients/{publicId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteClientAsync(string publicId)
    {
        var response = await DeleteAsync($"api/clients/{publicId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AddContactAsync(string clientPublicId, AddClientContactRequest request)
    {
        var response = await PostAsync($"api/clients/{clientPublicId}/contacts", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveContactAsync(string clientPublicId, string contactPublicId)
    {
        var response = await DeleteAsync($"api/clients/{clientPublicId}/contacts/{contactPublicId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AssignToGroupAsync(string clientPublicId, string groupPublicId)
    {
        var response = await PostAsync($"api/clients/{clientPublicId}/assign-group/{groupPublicId}", new { });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveFromGroupAsync(string clientPublicId)
    {
        var response = await PostAsync($"api/clients/{clientPublicId}/remove-group", new { });
        return response.IsSuccessStatusCode;
    }

    // Location management
    public async Task<LocationResponse?> CreateLocationAsync(CreateLocationRequest request)
    {
        var response = await PostAsync("api/locations", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LocationResponse>();
    }

    public async Task<bool> UpdateLocationAsync(string publicId, UpdateLocationRequest request)
    {
        var response = await PatchAsync($"api/locations/{publicId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ActivateLocationAsync(string publicId)
    {
        var response = await PostAsync($"api/locations/{publicId}/activate", new { });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeactivateLocationAsync(string publicId)
    {
        var response = await PostAsync($"api/locations/{publicId}/deactivate", new { });
        return response.IsSuccessStatusCode;
    }
}










