using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Comanda.Client.Admin.Components;
using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Infrastructure.ApiClients;
using Comanda.Client.Admin.Infrastructure.Notifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// MudBlazor
builder.Services.AddMudServices();

// HttpClient
builder.Services.AddScoped(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseUrl = config["ApiSettings:BaseUrl"] ?? "https://localhost:5001";
    return new HttpClient { BaseAddress = new Uri(baseUrl) };
});

// Authentication - Custom handler for initial connection authorization
// This allows [Authorize] attributes to work without blocking the initial connection
// Actual authentication is handled by CustomAuthStateProvider
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "BlazorScheme";
    options.DefaultChallengeScheme = "BlazorScheme";
})
.AddScheme<AuthenticationSchemeOptions, BlazorAuthenticationHandler>("BlazorScheme", options => { });

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();

// API Clients
builder.Services.AddScoped<AuthApiService>();
builder.Services.AddScoped<OrderApiService>();
builder.Services.AddScoped<ClientApiService>();
builder.Services.AddScoped<PersonApiService>();
builder.Services.AddScoped<AccountApiService>();
builder.Services.AddScoped<AuthorizationApiService>();
builder.Services.AddScoped<ProductApiService>();
builder.Services.AddScoped<BatchApiService>();
builder.Services.AddScoped<LedgerApiService>();
builder.Services.AddScoped<ExpenseApiService>();
builder.Services.AddScoped<EmployeeApiService>();

// Notifications
builder.Services.AddScoped<NotificationHubService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<Comanda.Client.Admin.App>()
    .AddInteractiveServerRenderMode();

app.Run();










