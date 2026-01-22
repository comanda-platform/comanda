namespace Comanda.Database;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Comanda.Database.Entities;

public class Context(DbContextOptions<Context> options) : IdentityDbContext<EmployeeDatabaseEntity, IdentityRole<int>, int>(options)
{
    public DbSet<AuditLogDatabaseEntity> AuditLogs { get; set; } = null!;
    public DbSet<ClientDatabaseEntity> Clients { get; set; } = null!;
    public DbSet<ClientContactDatabaseEntity> ClientContacts { get; set; } = null!;
    public DbSet<ClientGroupDatabaseEntity> ClientGroups { get; set; } = null!;
    public DbSet<ClientLedgerEntryDatabaseEntity> ClientLedgerEntries { get; set; } = null!;
    public DbSet<DailyMenuDatabaseEntity> DailyMenus { get; set; } = null!;
    public DbSet<DailyMenuItemDatabaseEntity> DailyMenuItems { get; set; } = null!;
    public DbSet<DailySideAvailabilityDatabaseEntity> DailySideAvailabilities { get; set; } = null!;
    public DbSet<EmployeeDatabaseEntity> Employees { get; set; } = null!;
    public DbSet<ExpenseDatabaseEntity> Expenses { get; set; } = null!;
    public DbSet<InventoryItemDatabaseEntity> InventoryItems { get; set; } = null!;
    public DbSet<InventoryPurchaseDatabaseEntity> InventoryPurchases { get; set; } = null!;
    public DbSet<InventoryPurchaseLineDatabaseEntity> InventoryPurchaseLines { get; set; } = null!;
    public DbSet<LocationDatabaseEntity> Locations { get; set; } = null!;
    public DbSet<NoteDatabaseEntity> Notes { get; set; } = null!;
    public DbSet<OrderDatabaseEntity> Orders { get; set; } = null!;
    public DbSet<OrderLineDatabaseEntity> OrderLines { get; set; } = null!;
    public DbSet<OrderLineOptionDatabaseEntity> OrderLineOptions { get; set; } = null!;
    public DbSet<OrderLineSideDatabaseEntity> OrderLineSides { get; set; } = null!;
    public DbSet<OrderStatusHistoryDatabaseEntity> OrderStatusHistories { get; set; } = null!;
    public DbSet<ProductDatabaseEntity> Products { get; set; } = null!;
    public DbSet<ProductPriceHistoryDatabaseEntity> ProductPriceHistories { get; set; } = null!;
    public DbSet<ProductTypeDatabaseEntity> ProductTypes { get; set; } = null!;
    public DbSet<RecipeDatabaseEntity> Recipes { get; set; } = null!;
    public DbSet<RecipeIngredientDatabaseEntity> RecipeIngredients { get; set; } = null!;
    public DbSet<SideDatabaseEntity> Sides { get; set; } = null!;
    public DbSet<SupplierDatabaseEntity> Suppliers { get; set; } = null!;
    public DbSet<UnitDatabaseEntity> Units { get; set; } = null!;
    public DbSet<ProductionBatchDatabaseEntity> ProductionBatches { get; set; } = null!;

    // New Account Model entities
    public DbSet<PersonDatabaseEntity> Persons { get; set; } = null!;
    public DbSet<PersonContactDatabaseEntity> PersonContacts { get; set; } = null!;
    public DbSet<AccountDatabaseEntity> Accounts { get; set; } = null!;
    public DbSet<AuthorizationDatabaseEntity> Authorizations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Global query filters for soft delete
        modelBuilder.Entity<ClientDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ClientContactDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ClientGroupDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ClientLedgerEntryDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<DailyMenuDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<DailyMenuItemDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<EmployeeDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ExpenseDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<InventoryItemDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<InventoryPurchaseDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<InventoryPurchaseLineDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<LocationDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<OrderDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<OrderLineDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ProductDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ProductTypeDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<RecipeDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<RecipeIngredientDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<SideDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<SupplierDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);

        // New Account Model soft delete filters
        modelBuilder.Entity<PersonDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<PersonContactDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<AccountDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<AuthorizationDatabaseEntity>().HasQueryFilter(e => !e.IsDeleted);

        // Configure Order -> Client relationships to avoid multiple cascade paths
        modelBuilder.Entity<OrderDatabaseEntity>()
            .HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderDatabaseEntity>()
            .HasOne(o => o.CreatedByClient)
            .WithMany()
            .HasForeignKey(o => o.CreatedByClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure AuditLog -> Client relationship
        modelBuilder.Entity<AuditLogDatabaseEntity>()
            .HasOne(a => a.ChangedByClient)
            .WithMany()
            .HasForeignKey(a => a.ChangedByClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure OrderStatusHistory -> Client relationship
        modelBuilder.Entity<OrderStatusHistoryDatabaseEntity>()
            .HasOne(h => h.ChangedByClient)
            .WithMany()
            .HasForeignKey(h => h.ChangedByClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Employee self-referencing relationships
        modelBuilder.Entity<EmployeeDatabaseEntity>()
            .HasOne(e => e.CreatedBy)
            .WithMany()
            .HasForeignKey(e => e.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmployeeDatabaseEntity>()
            .HasOne(e => e.LastModifiedBy)
            .WithMany()
            .HasForeignKey(e => e.LastModifiedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmployeeDatabaseEntity>()
            .HasOne(e => e.DeletedBy)
            .WithMany()
            .HasForeignKey(e => e.DeletedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure OrderLineSide composite primary key (join table)
        modelBuilder.Entity<OrderLineSideDatabaseEntity>()
            .HasKey(ols => new { ols.OrderLineId, ols.SideId });

        modelBuilder.Entity<OrderLineSideDatabaseEntity>()
            .HasOne(ols => ols.OrderLine)
            .WithMany()
            .HasForeignKey(ols => ols.OrderLineId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderLineSideDatabaseEntity>()
            .HasOne(ols => ols.Side)
            .WithMany(s => s.OrderLineSides)
            .HasForeignKey(ols => ols.SideId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure ClientGroup -> Employee audit relationships
        modelBuilder.Entity<ClientGroupDatabaseEntity>()
            .HasOne(cg => cg.CreatedBy)
            .WithMany()
            .HasForeignKey(cg => cg.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClientGroupDatabaseEntity>()
            .HasOne(cg => cg.LastModifiedBy)
            .WithMany()
            .HasForeignKey(cg => cg.LastModifiedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClientGroupDatabaseEntity>()
            .HasOne(cg => cg.DeletedBy)
            .WithMany()
            .HasForeignKey(cg => cg.DeletedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure ClientLedgerEntry -> Employee audit relationships
        modelBuilder.Entity<ClientLedgerEntryDatabaseEntity>()
            .HasOne(cle => cle.CreatedBy)
            .WithMany()
            .HasForeignKey(cle => cle.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClientLedgerEntryDatabaseEntity>()
            .HasOne(cle => cle.LastModifiedBy)
            .WithMany()
            .HasForeignKey(cle => cle.LastModifiedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClientLedgerEntryDatabaseEntity>()
            .HasOne(cle => cle.DeletedBy)
            .WithMany()
            .HasForeignKey(cle => cle.DeletedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure RecipeIngredient relationships to avoid cascade cycles
        modelBuilder.Entity<RecipeIngredientDatabaseEntity>()
            .HasOne(ri => ri.Unit)
            .WithMany(u => u.RecipeIngredients)
            .HasForeignKey(ri => ri.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeIngredientDatabaseEntity>()
            .HasOne(ri => ri.Item)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.InventoryItemId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure InventoryItem -> BaseUnit to avoid cascade cycles
        modelBuilder.Entity<InventoryItemDatabaseEntity>()
            .HasOne(i => i.BaseUnit)
            .WithMany(u => u.Items)
            .HasForeignKey(i => i.BaseUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure InventoryPurchaseLine -> Unit to avoid cascade cycles
        modelBuilder.Entity<InventoryPurchaseLineDatabaseEntity>()
            .HasOne(ipl => ipl.Unit)
            .WithMany(u => u.PurchaseLines)
            .HasForeignKey(ipl => ipl.UnitId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}







