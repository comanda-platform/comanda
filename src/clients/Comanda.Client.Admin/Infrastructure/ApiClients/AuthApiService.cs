using Comanda.Client.Admin.Models;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class AuthApiService(HttpClient httpClient, IConfiguration configuration) : AdminApiClient(httpClient, null!, configuration)
{
    public async Task<AuthResponse?> LoginAsync(string email, string password)
    {
        var request = new LoginRequest(email, password);
        var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/api/auth/login", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AuthResponse>();
    }
}










