namespace Comanda.Api.Extensions;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Application.UseCases;
using Comanda.Infrastructure.Adapters;
using Comanda.Infrastructure.Services;
using DbRepos = Infrastructure.Database.Repositories;
using DomainRepos = Domain.Repositories;
using Comanda.Application.Services;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices()
        {
            // Application Services
            services.AddScoped<ProductTypeUseCase>();
            services.AddScoped<ProductUseCase>();
            services.AddScoped<ClientUseCase>();
            services.AddScoped<ClientGroupUseCase>();
            services.AddScoped<PersonUseCase>();
            services.AddScoped<AccountUseCase>();
            services.AddScoped<AuthorizationUseCase>();
            services.AddScoped<LocationUseCase>();
            services.AddScoped<EmployeeUseCase>();
            services.AddScoped<OrderUseCase>();
            services.AddScoped<UnitUseCase>();
            services.AddScoped<InventoryItemUseCase>();
            services.AddScoped<SupplierUseCase>();
            services.AddScoped<InventoryPurchaseUseCase>();
            services.AddScoped<SideUseCase>();
            services.AddScoped<RecipeUseCase>();
            services.AddScoped<DailyMenuUseCase>();
            services.AddScoped<DailySideAvailabilityUseCase>();
            services.AddScoped<ExpenseUseCase>();
            services.AddScoped<NoteUseCase>();
            services.AddScoped<LedgerEntryUseCase>();
            services.AddScoped<AuthenticationUseCase>();
            services.AddScoped<ProductionBatchUseCase>();

            return services;
        }

        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            // Database Context
            services.AddDbContext<Context>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(configuration.GetConnectionString("AppDbConnection")));

            // Database Repositories (Infrastructure layer)
            services.AddScoped<DbRepos.IProductTypeRepository, DbRepos.ProductTypeRepository>();
            services.AddScoped<DbRepos.IProductRepository, DbRepos.ProductRepository>();
            services.AddScoped<DbRepos.IProductPriceHistoryRepository, DbRepos.ProductPriceHistoryRepository>();
            services.AddScoped<DbRepos.IClientRepository, DbRepos.ClientRepository>();
            services.AddScoped<DbRepos.IClientGroupRepository, DbRepos.ClientGroupRepository>();
            services.AddScoped<DbRepos.IPersonRepository, DbRepos.PersonRepository>();
            services.AddScoped<DbRepos.IAccountRepository, DbRepos.AccountRepository>();
            services.AddScoped<DbRepos.IAuthorizationRepository, DbRepos.AuthorizationRepository>();
            services.AddScoped<DbRepos.ILocationRepository, DbRepos.LocationRepository>();
            services.AddScoped<DbRepos.IEmployeeRepository, DbRepos.EmployeeRepository>();
            services.AddScoped<DbRepos.IOrderRepository, DbRepos.OrderRepository>();
            services.AddScoped<DbRepos.ILedgerEntryRepository, DbRepos.LedgerEntryRepository>();
            services.AddScoped<DbRepos.IUnitRepository, DbRepos.UnitRepository>();
            services.AddScoped<DbRepos.IInventoryItemRepository, DbRepos.InventoryItemRepository>();
            services.AddScoped<DbRepos.ISupplierRepository, DbRepos.SupplierRepository>();
            services.AddScoped<DbRepos.IInventoryPurchaseRepository, DbRepos.InventoryPurchaseRepository>();
            services.AddScoped<DbRepos.ISideRepository, DbRepos.SideRepository>();
            services.AddScoped<DbRepos.IRecipeRepository, DbRepos.RecipeRepository>();
            services.AddScoped<DbRepos.IDailyMenuRepository, DbRepos.DailyMenuRepository>();
            services.AddScoped<DbRepos.IDailySideAvailabilityRepository, DbRepos.DailySideAvailabilityRepository>();
            services.AddScoped<DbRepos.IExpenseRepository, DbRepos.ExpenseRepository>();
            services.AddScoped<DbRepos.INoteRepository, DbRepos.NoteRepository>();
            services.AddScoped<DbRepos.IProductionBatchRepository, DbRepos.ProductionBatchRepository>();

            // Infrastructure Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            // Domain Repository Adapters (bridges Domain and Infrastructure)
            services.AddScoped<DomainRepos.IProductTypeRepository, ProductTypeRepositoryAdapter>();
            services.AddScoped<DomainRepos.IProductRepository, ProductRepositoryAdapter>();
            services.AddScoped<DomainRepos.IClientRepository, ClientRepositoryAdapter>();
            services.AddScoped<DomainRepos.IClientGroupRepository, ClientGroupRepositoryAdapter>();
            services.AddScoped<DomainRepos.IPersonRepository, PersonRepositoryAdapter>();
            services.AddScoped<DomainRepos.IAccountRepository, AccountRepositoryAdapter>();
            services.AddScoped<DomainRepos.IAuthorizationRepository, AuthorizationRepositoryAdapter>();
            services.AddScoped<DomainRepos.ILocationRepository, LocationRepositoryAdapter>();
            services.AddScoped<DomainRepos.IEmployeeRepository, EmployeeRepositoryAdapter>();
            services.AddScoped<DomainRepos.IOrderRepository, OrderRepositoryAdapter>();
            services.AddScoped<DomainRepos.ILedgerEntryRepository, LedgerEntryRepositoryAdapter>();
            services.AddScoped<DomainRepos.IUnitRepository, UnitRepositoryAdapter>();
            services.AddScoped<DomainRepos.IInventoryItemRepository, InventoryItemRepositoryAdapter>();
            services.AddScoped<DomainRepos.ISupplierRepository, SupplierRepositoryAdapter>();
            services.AddScoped<DomainRepos.IInventoryPurchaseRepository, InventoryPurchaseRepositoryAdapter>();
            services.AddScoped<DomainRepos.ISideRepository, SideRepositoryAdapter>();
            services.AddScoped<DomainRepos.IRecipeRepository, RecipeRepositoryAdapter>();
            services.AddScoped<DomainRepos.IDailyMenuRepository, DailyMenuRepositoryAdapter>();
            services.AddScoped<DomainRepos.IDailySideAvailabilityRepository, DailySideAvailabilityRepositoryAdapter>();
            services.AddScoped<DomainRepos.IExpenseRepository, ExpenseRepositoryAdapter>();
            services.AddScoped<DomainRepos.INoteRepository, NoteRepositoryAdapter>();
            services.AddScoped<DomainRepos.IProductionBatchRepository, ProductionBatchRepositoryAdapter>();

            return services;
        }
    }
}







