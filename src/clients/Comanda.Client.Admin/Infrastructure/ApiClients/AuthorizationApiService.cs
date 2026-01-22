using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class AuthorizationApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    public async Task<IEnumerable<AuthorizationResponse>> GetAuthorizationsAsync(
        string? personPublicId = null,
        string? accountPublicId = null,
        bool? activeOnly = null)
    {
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(personPublicId))
            queryParams.Add($"personPublicId={personPublicId}");
        if (!string.IsNullOrEmpty(accountPublicId))
            queryParams.Add($"accountPublicId={accountPublicId}");
        if (activeOnly.HasValue)
            queryParams.Add($"activeOnly={activeOnly.Value}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

        return await GetAsync<IEnumerable<AuthorizationResponse>>($"api/authorizations{query}") ?? [];
    }

    public async Task<AuthorizationResponse?> GetAuthorizationByIdAsync(string publicId)
    {
        return await GetAsync<AuthorizationResponse>($"api/authorizations/{publicId}");
    }

    public async Task<AuthorizationResponse?> CreateAuthorizationAsync(CreateAuthorizationRequest request)
    {
        var response = await PostAsync("api/authorizations", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AuthorizationResponse>();
    }

    public async Task<bool> UpdateAuthorizationAsync(string publicId, PatchAuthorizationRequest request)
    {
        var response = await PatchAsync($"api/authorizations/{publicId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAuthorizationAsync(string publicId)
    {
        var response = await DeleteAsync($"api/authorizations/{publicId}");
        return response.IsSuccessStatusCode;
    }
}
