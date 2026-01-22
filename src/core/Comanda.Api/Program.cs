using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Comanda.Api.Notifications;
using Comanda.Api.Authentication;
using Comanda.Api.Endpoints;
using Comanda.Api.Extensions;
using Comanda.Api.Notifications;
using Comanda.Api.Services;
using Comanda.Application.Notifications;
using Comanda.Infrastructure.Extensions;
using Comanda.Infrastructure.Notifications;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure (Database Context, Repositories, Services)
builder.Services.AddInfrastructure(builder.Configuration);

// Identity (encapsulated in Infrastructure layer)
builder.Services.AddIdentityServices();

// Notifications
{
    builder.Services.AddSignalR();
    builder.Services.AddSingleton<INotificationQueue, InMemoryNotificationQueue>();
    builder.Services.AddScoped<INotificationPublisher, QueueNotificationPublisher>();
    builder.Services.AddSingleton<INotificationDispatcher, SignalRNotificationDispatcher>();
    builder.Services.AddHostedService<NotificationWorker>();
}

// Application Services
builder.Services.AddApplicationServices();

// Database Seeder
builder.Services.AddScoped<DatabaseSeeder>();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
var key = Encoding.UTF8.GetBytes(jwtKey!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
})
.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
    ApiKeyAuthenticationOptions.DefaultScheme,
    options => { });

builder.Services.AddAuthorization();

// OpenAPI Configuration (.NET 10 approach)
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new Microsoft.OpenApi.OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, Microsoft.OpenApi.IOpenApiSecurityScheme>();

        // Add JWT Bearer security scheme
        document.Components.SecuritySchemes.Add("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
        {
            Type = Microsoft.OpenApi.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below."
        });

        // Add API Key security scheme
        document.Components.SecuritySchemes.Add("ApiKey", new Microsoft.OpenApi.OpenApiSecurityScheme
        {
            Type = Microsoft.OpenApi.SecuritySchemeType.ApiKey,
            Name = "X-Api-Key",
            In = Microsoft.OpenApi.ParameterLocation.Header,
            Description = "API Key Authentication. Enter your API Key in the X-Api-Key header."
        });

        // Apply global security requirements
        document.Security =
        [
            new Microsoft.OpenApi.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.OpenApiSecuritySchemeReference("Bearer"),
                    new List<string>()
                }
            },
            new Microsoft.OpenApi.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.OpenApiSecuritySchemeReference("ApiKey"),
                    new List<string>()
                }
            }
        ];

        document.SetReferenceHostDocument();
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Seed database in development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}

// OpenAPI and Swagger UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Comanda.Api V1");
        options.RoutePrefix = "swagger";
        options.DocumentTitle = "Comanda.Api";
        options.DisplayRequestDuration();
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalExceptionHandling();

// SignalR Hub
app.MapHub<Comanda.Api.SignalR.AppHub>("/hub");

// Auth endpoints
app.MapAuthEndpoints();
app.MapAuthApiKeyEndpoints();
app.MapUserEndpoints();
app.MapProtectedEndpoints();

// Business endpoints
app.MapProductTypeEndpoints();
app.MapProductEndpoints();
app.MapClientGroupEndpoints();
app.MapClientEndpoints();
app.MapPersonEndpoints();
app.MapAccountEndpoints();
app.MapAuthorizationEndpoints();
app.MapLocationEndpoints();
app.MapEmployeeEndpoints();
app.MapOrderEndpoints();
app.MapUnitEndpoints();
app.MapInventoryItemEndpoints();
app.MapSupplierEndpoints();
app.MapInventoryPurchaseEndpoints();
app.MapSideEndpoints();
app.MapRecipeEndpoints();
app.MapDailyMenuEndpoints();
app.MapDailySideAvailabilityEndpoints();
app.MapExpenseEndpoints();
app.MapNoteEndpoints();
app.MapLedgerEntryEndpoints();
app.MapProductionBatchEndpoints();

app.Run();







