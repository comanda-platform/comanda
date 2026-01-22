using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class PersonApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    public async Task<IEnumerable<PersonResponse>> GetPersonsAsync()
    {
        return await GetAsync<IEnumerable<PersonResponse>>("api/persons") ?? [];
    }

    public async Task<PersonResponse?> GetPersonByIdAsync(string publicId)
    {
        return await GetAsync<PersonResponse>($"api/persons/{publicId}");
    }

    public async Task<PersonResponse?> CreatePersonAsync(CreatePersonRequest request)
    {
        var response = await PostAsync("api/persons", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<PersonResponse>();
    }

    public async Task<bool> UpdatePersonAsync(string publicId, UpdatePersonRequest request)
    {
        var response = await PatchAsync($"api/persons/{publicId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletePersonAsync(string publicId)
    {
        var response = await DeleteAsync($"api/persons/{publicId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AddContactAsync(string personPublicId, AddPersonContactRequest request)
    {
        var response = await PostAsync($"api/persons/{personPublicId}/contacts", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveContactAsync(string personPublicId, string contactPublicId)
    {
        var response = await DeleteAsync($"api/persons/{personPublicId}/contacts/{contactPublicId}");
        return response.IsSuccessStatusCode;
    }
}
