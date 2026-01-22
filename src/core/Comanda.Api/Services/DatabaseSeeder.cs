namespace Comanda.Api.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;
using Comanda.Domain.Helpers;
using Comanda.Shared.Enums;

public class DatabaseSeeder(
    Context context,
    UserManager<EmployeeDatabaseEntity> userManager)
{
    private readonly Context _context = context;
    private readonly UserManager<EmployeeDatabaseEntity> _userManager = userManager;

    public async Task SeedAsync()
    {
        // Ensure database is created
        await _context.Database.EnsureCreatedAsync();

        // Check if already seeded
        if (await _context.Employees.AnyAsync())
            return;

        var now = DateTime.UtcNow;

        // 1. Create Employees
        var employees = await SeedEmployeesAsync(now);
        var adminEmployee = employees.First();

        // 2. Create Units
        var units = await SeedUnitsAsync();

        // 3. Create Product Types
        var productTypes = await SeedProductTypesAsync(adminEmployee, now);

        // 4. Create Products
        var products = await SeedProductsAsync(productTypes, adminEmployee, now);

        // 5. Create Inventory Items
        var inventoryItems = await SeedInventoryItemsAsync(units, adminEmployee, now);

        // 6. Create Recipes
        await SeedRecipesAsync(products, inventoryItems, units, adminEmployee, now);

        // 7. Create Sides
        var sides = await SeedSidesAsync(adminEmployee, now);

        // 8. Create Location
        var location = await SeedLocationAsync(adminEmployee, now);

        // 9. Create Daily Menu for today
        await SeedDailyMenuAsync(products, location, adminEmployee, now);

        // 10. Create Daily Side Availability
        await SeedDailySideAvailabilityAsync(sides, location, adminEmployee, now);

        // 11. Create Sample Orders
        await SeedOrdersAsync(products, location, adminEmployee, now);

        await _context.SaveChangesAsync();
    }

    private async Task<List<EmployeeDatabaseEntity>> SeedEmployeesAsync(DateTime now)
    {
        var employees = new List<EmployeeDatabaseEntity>();

        // Admin employee
        var admin = new EmployeeDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            UserName = "admin@Comanda.com",
            Email = "admin@Comanda.com",
            EmailConfirmed = true,
            CreatedAt = now,
            ApiKey = "kitchen-api-key-12345"
        };

        var result = await _userManager.CreateAsync(admin, "Admin123!");
        if (result.Succeeded)
        {
            employees.Add(admin);
        }
        else
        {
            throw new InvalidOperationException($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Kitchen staff
        var kitchenStaff = new EmployeeDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            UserName = "cocina@Comanda.com",
            Email = "cocina@Comanda.com",
            EmailConfirmed = true,
            CreatedAt = now,
            ApiKey = "cocina-api-key-67890"
        };

        result = await _userManager.CreateAsync(kitchenStaff, "Cocina123!");
        if (result.Succeeded)
        {
            employees.Add(kitchenStaff);
        }
        else
        {
            throw new InvalidOperationException($"Failed to create kitchen staff user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        return employees;
    }

    private async Task<Dictionary<string, UnitDatabaseEntity>> SeedUnitsAsync()
    {
        var units = new Dictionary<string, UnitDatabaseEntity>
        {
            ["g"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Code = "g",
                Name = "Gram",
                CategoryId = (int)UnitCategory.Weight,
                ToBaseMultiplier = 1
            },
            ["kg"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Code = "kg",
                Name = "Kilogram",
                CategoryId = (int)UnitCategory.Weight,
                ToBaseMultiplier = 1000
            },
            ["ml"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Code = "ml",
                Name = "Milliliter",
                CategoryId = (int)UnitCategory.Volume,
                ToBaseMultiplier = 1
            },
            ["l"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Code = "l",
                Name = "Liter",
                CategoryId = (int)UnitCategory.Volume,
                ToBaseMultiplier = 1000
            },
            ["pz"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Code = "pz",
                Name = "Piece",
                CategoryId = (int)UnitCategory.Count,
                ToBaseMultiplier = 1
            }
        };

        _context.Units.AddRange(units.Values);
        await _context.SaveChangesAsync();

        return units;
    }

    private async Task<Dictionary<string, ProductTypeDatabaseEntity>> SeedProductTypesAsync(
        EmployeeDatabaseEntity admin,
        DateTime now)
    {
        var types = new Dictionary<string, ProductTypeDatabaseEntity>
        {
            ["MainDish"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Plato Fuerte",
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Side"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Guarnición",
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Beverage"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Bebida",
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Dessert"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Postre",
                CreatedAt = now,
                CreatedById = admin.Id
            }
        };

        _context.ProductTypes.AddRange(types.Values);
        await _context.SaveChangesAsync();

        return types;
    }

    private async Task<Dictionary<string, ProductDatabaseEntity>> SeedProductsAsync(
        Dictionary<string, ProductTypeDatabaseEntity> productTypes,
        EmployeeDatabaseEntity admin,
        DateTime now)
    {
        var products = new Dictionary<string, ProductDatabaseEntity>
        {
            ["Lasagna"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Lasaña de Carne",
                Description = "Lasaña tradicional italiana con carne molida y queso",
                Price = 85.00m,
                IsSuggestedForDailyMenu = true,
                ProductTypeId = productTypes["MainDish"].Id,
                Type = productTypes["MainDish"],
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["ChickenMilanesa"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Milanesa de Pollo",
                Description = "Pechuga de pollo empanizada con guarnición",
                Price = 75.00m,
                IsSuggestedForDailyMenu = true,
                ProductTypeId = productTypes["MainDish"].Id,
                Type = productTypes["MainDish"],
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["BeefStew"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Guisado de Res",
                Description = "Estofado de res con verduras en salsa roja",
                Price = 80.00m,
                IsSuggestedForDailyMenu = true,
                ProductTypeId = productTypes["MainDish"].Id,
                Type = productTypes["MainDish"],
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["FishTacos"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Tacos de Pescado",
                Description = "Tres tacos de pescado empanizado con salsa",
                Price = 70.00m,
                IsSuggestedForDailyMenu = true,
                ProductTypeId = productTypes["MainDish"].Id,
                Type = productTypes["MainDish"],
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["ChickenSoup"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Caldo de Pollo",
                Description = "Caldo tradicional mexicano con pollo y verduras",
                Price = 65.00m,
                IsSuggestedForDailyMenu = true,
                ProductTypeId = productTypes["MainDish"].Id,
                Type = productTypes["MainDish"],
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Rice"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Arroz Rojo",
                Price = 15.00m,
                ProductTypeId = productTypes["Side"].Id,
                Type = productTypes["Side"],
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Agua"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Agua de Horchata",
                Price = 25.00m,
                ProductTypeId = productTypes["Beverage"].Id,
                Type = productTypes["Beverage"],
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Flan"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Flan Napolitano",
                Price = 35.00m,
                ProductTypeId = productTypes["Dessert"].Id,
                Type = productTypes["Dessert"],
                CreatedAt = now,
                CreatedById = admin.Id
            }
        };

        _context.Products.AddRange(products.Values);
        await _context.SaveChangesAsync();

        return products;
    }

    private async Task<Dictionary<string, InventoryItemDatabaseEntity>> SeedInventoryItemsAsync(
        Dictionary<string, UnitDatabaseEntity> units,
        EmployeeDatabaseEntity admin,
        DateTime now)
    {
        var items = new Dictionary<string, InventoryItemDatabaseEntity>
        {
            ["Chicken"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Pollo",
                BaseUnitId = units["kg"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Beef"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Carne de Res",
                BaseUnitId = units["kg"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Fish"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Pescado",
                BaseUnitId = units["kg"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Pasta"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Pasta para Lasaña",
                BaseUnitId = units["g"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Cheese"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Queso Mozzarella",
                BaseUnitId = units["g"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Rice"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Arroz",
                BaseUnitId = units["kg"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Tomato"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Jitomate",
                BaseUnitId = units["kg"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Onion"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Cebolla",
                BaseUnitId = units["kg"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Oil"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Aceite Vegetal",
                BaseUnitId = units["l"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Tortillas"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Tortillas de Maíz",
                BaseUnitId = units["pz"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            ["Breadcrumbs"] = new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Pan Molido",
                BaseUnitId = units["g"].Id,
                CreatedAt = now,
                CreatedById = admin.Id
            }
        };

        _context.InventoryItems.AddRange(items.Values);
        await _context.SaveChangesAsync();

        return items;
    }

    private async Task SeedRecipesAsync(
        Dictionary<string, ProductDatabaseEntity> products,
        Dictionary<string, InventoryItemDatabaseEntity> items,
        Dictionary<string, UnitDatabaseEntity> units,
        EmployeeDatabaseEntity admin,
        DateTime now)
    {
        var recipes = new List<RecipeDatabaseEntity>
        {
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Receta Lasaña de Carne",
                ProductId = products["Lasagna"].Id,
                CreatedAt = now,
                CreatedById = admin.Id,
                EstimatedPortions = 12,
                Ingredients =
                [
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Beef"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 200,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Pasta"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 150,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Cheese"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 100,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Tomato"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 150,
                        CreatedAt = now
                    }
                ]
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Receta Milanesa de Pollo",
                ProductId = products["ChickenMilanesa"].Id,
                CreatedAt = now,
                CreatedById = admin.Id,
                EstimatedPortions = 15,
                Ingredients =
                [
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Chicken"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 250,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Breadcrumbs"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 50,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Oil"].Id,
                        UnitId = units["ml"].Id,
                        Quantity = 100,
                        CreatedAt = now
                    }
                ]
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Receta Guisado de Res",
                ProductId = products["BeefStew"].Id,
                CreatedAt = now,
                CreatedById = admin.Id,
                EstimatedPortions = 18,
                Ingredients =
                [
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Beef"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 300,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Tomato"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 200,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Onion"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 100,
                        CreatedAt = now
                    }
                ]
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Receta Tacos de Pescado",
                ProductId = products["FishTacos"].Id,
                CreatedAt = now,
                CreatedById = admin.Id,
                EstimatedPortions = 20,
                Ingredients =
                [
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Fish"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 200,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Tortillas"].Id,
                        UnitId = units["pz"].Id,
                        Quantity = 3,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Breadcrumbs"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 30,
                        CreatedAt = now
                    }
                ]
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Receta Caldo de Pollo",
                ProductId = products["ChickenSoup"].Id,
                CreatedAt = now,
                CreatedById = admin.Id,
                EstimatedPortions = 25,
                Ingredients =
                [
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Chicken"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 350,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Rice"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 50,
                        CreatedAt = now
                    },
                    new RecipeIngredientDatabaseEntity
                    {
                        PublicId = PublicIdHelper.Generate(),
                        InventoryItemId = items["Onion"].Id,
                        UnitId = units["g"].Id,
                        Quantity = 50,
                        CreatedAt = now
                    }
                ]
            }
        };

        _context.Recipes.AddRange(recipes);
        await _context.SaveChangesAsync();
    }

    private async Task<List<SideDatabaseEntity>> SeedSidesAsync(
        EmployeeDatabaseEntity admin,
        DateTime now)
    {
        var sides = new List<SideDatabaseEntity>
        {
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Arroz Rojo",
                IsActive = true,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Frijoles Refritos",
                IsActive = true,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Ensalada Verde",
                IsActive = true,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Tortillas de Maíz",
                IsActive = true,
                CreatedAt = now,
                CreatedById = admin.Id
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                Name = "Totopos con Salsa",
                IsActive = true,
                CreatedAt = now,
                CreatedById = admin.Id
            }
        };

        _context.Sides.AddRange(sides);
        await _context.SaveChangesAsync();

        return sides;
    }

    private async Task<LocationDatabaseEntity> SeedLocationAsync(
        EmployeeDatabaseEntity admin,
        DateTime now)
    {
        var location = new LocationDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            Name = "Cocina Principal",
            LocationTypeId = (int)LocationType.OurKitchen,
            IsActive = true,
            AddressLine = "Calle Principal #123, Centro",
            CreatedAt = now,
            CreatedById = admin.Id
        };

        _context.Locations.Add(location);
        await _context.SaveChangesAsync();

        return location;
    }

    private async Task SeedDailyMenuAsync(
        Dictionary<string, ProductDatabaseEntity> products,
        LocationDatabaseEntity location,
        EmployeeDatabaseEntity admin,
        DateTime now)
    {
        var today = DateOnly.FromDateTime(now);

        // Create today's menu
        var menu = new DailyMenuDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            Date = today,
            LocationId = location.Id,
            CreatedAt = now,
            CreatedById = admin.Id
        };

        _context.DailyMenus.Add(menu);
        await _context.SaveChangesAsync();

        // Add menu items for today
        var todayItems = new List<DailyMenuItemDatabaseEntity>
        {
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                DailyMenuId = menu.Id,
                Menu = menu,
                ProductId = products["Lasagna"].Id,
                Product = products["Lasagna"],
                SequenceOrder = 1,
                CreatedAt = now
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                DailyMenuId = menu.Id,
                Menu = menu,
                ProductId = products["ChickenMilanesa"].Id,
                Product = products["ChickenMilanesa"],
                SequenceOrder = 2,
                CreatedAt = now
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                DailyMenuId = menu.Id,
                Menu = menu,
                ProductId = products["BeefStew"].Id,
                Product = products["BeefStew"],
                SequenceOrder = 3,
                CreatedAt = now
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                DailyMenuId = menu.Id,
                Menu = menu,
                ProductId = products["FishTacos"].Id,
                Product = products["FishTacos"],
                SequenceOrder = 4,
                CreatedAt = now
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                DailyMenuId = menu.Id,
                Menu = menu,
                ProductId = products["ChickenSoup"].Id,
                Product = products["ChickenSoup"],
                SequenceOrder = 5,
                CreatedAt = now
            }
        };

        _context.DailyMenuItems.AddRange(todayItems);
        await _context.SaveChangesAsync();

        // Create tomorrow's menu
        var tomorrow = today.AddDays(1);
        var menuTomorrow = new DailyMenuDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            Date = tomorrow,
            LocationId = location.Id,
            CreatedAt = now,
            CreatedById = admin.Id
        };

        _context.DailyMenus.Add(menuTomorrow);
        await _context.SaveChangesAsync();

        // Add menu items for tomorrow
        var tomorrowItems = new List<DailyMenuItemDatabaseEntity>
        {
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                DailyMenuId = menuTomorrow.Id,
                Menu = menuTomorrow,
                ProductId = products["ChickenSoup"].Id,
                Product = products["ChickenSoup"],
                SequenceOrder = 1,
                OverriddenName = "Caldo de Pollo Especial",
                CreatedAt = now
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                DailyMenuId = menuTomorrow.Id,
                Menu = menuTomorrow,
                ProductId = products["BeefStew"].Id,
                Product = products["BeefStew"],
                SequenceOrder = 2,
                CreatedAt = now
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                DailyMenuId = menuTomorrow.Id,
                Menu = menuTomorrow,
                ProductId = products["Lasagna"].Id,
                Product = products["Lasagna"],
                SequenceOrder = 3,
                CreatedAt = now
            }
        };

        _context.DailyMenuItems.AddRange(tomorrowItems);
        await _context.SaveChangesAsync();
    }

    private async Task SeedDailySideAvailabilityAsync(
        List<SideDatabaseEntity> sides,
        LocationDatabaseEntity location,
        EmployeeDatabaseEntity admin,
        DateTime now)
    {
        var today = DateOnly.FromDateTime(now);

        var availabilities = sides.Select(side => new DailySideAvailabilityDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            SideId = side.Id,
            Date = today,
            IsAvailable = true,
            CreatedAt = now,
            CreatedById = admin.Id
        }).ToList();

        _context.DailySideAvailabilities.AddRange(availabilities);
        await _context.SaveChangesAsync();
    }

    private async Task SeedOrdersAsync(
        Dictionary<string, ProductDatabaseEntity> products,
        LocationDatabaseEntity location,
        EmployeeDatabaseEntity admin,
        DateTime now)
    {
        // Order 1: Dine-in, Created status
        var order1 = new OrderDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            OrderStatusTypeId = (int)OrderStatus.Created,
            FulfillmentTypeId = (int)OrderFulfillmentType.DineIn,
            OrderSourceId = (int)OrderSource.InPerson,
            LocationId = location.Id,
            CreatedAt = now.AddMinutes(-15),
            CreatedById = admin.Id
        };

        // Order 2: Take-away, Created status
        var order2 = new OrderDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            OrderStatusTypeId = (int)OrderStatus.Created,
            FulfillmentTypeId = (int)OrderFulfillmentType.TakeAway,
            OrderSourceId = (int)OrderSource.WhatsApp,
            LocationId = location.Id,
            CreatedAt = now.AddMinutes(-10),
            CreatedById = admin.Id
        };

        // Order 3: Delivery, Accepted status
        var order3 = new OrderDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            OrderStatusTypeId = (int)OrderStatus.Accepted,
            FulfillmentTypeId = (int)OrderFulfillmentType.Delivery,
            OrderSourceId = (int)OrderSource.Telephone,
            LocationId = location.Id,
            CreatedAt = now.AddMinutes(-25),
            CreatedById = admin.Id
        };

        // Order 4: Dine-in, Preparing status
        var order4 = new OrderDatabaseEntity
        {
            PublicId = PublicIdHelper.Generate(),
            OrderStatusTypeId = (int)OrderStatus.Preparing,
            FulfillmentTypeId = (int)OrderFulfillmentType.DineIn,
            OrderSourceId = (int)OrderSource.InPerson,
            LocationId = location.Id,
            CreatedAt = now.AddMinutes(-30),
            CreatedById = admin.Id
        };

        _context.Orders.AddRange(order1, order2, order3, order4);
        await _context.SaveChangesAsync();

        // Add order lines
        var orderLines = new List<OrderLineDatabaseEntity>
        {
            // Order 1 lines
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                OrderId = order1.Id,
                Order = order1,
                ProductId = products["Lasagna"].Id,
                Product = products["Lasagna"],
                Quantity = 2,
                UnitPrice = products["Lasagna"].Price,
                CreatedAt = now.AddMinutes(-15)
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                OrderId = order1.Id,
                Order = order1,
                ProductId = products["Agua"].Id,
                Product = products["Agua"],
                Quantity = 2,
                UnitPrice = products["Agua"].Price,
                CreatedAt = now.AddMinutes(-15)
            },
            // Order 2 lines
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                OrderId = order2.Id,
                Order = order2,
                ProductId = products["ChickenMilanesa"].Id,
                Product = products["ChickenMilanesa"],
                Quantity = 1,
                UnitPrice = products["ChickenMilanesa"].Price,
                CreatedAt = now.AddMinutes(-10)
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                OrderId = order2.Id,
                Order = order2,
                ProductId = products["Rice"].Id,
                Product = products["Rice"],
                Quantity = 1,
                UnitPrice = products["Rice"].Price,
                CreatedAt = now.AddMinutes(-10)
            },
            // Order 3 lines
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                OrderId = order3.Id,
                Order = order3,
                ProductId = products["BeefStew"].Id,
                Product = products["BeefStew"],
                Quantity = 3,
                UnitPrice = products["BeefStew"].Price,
                CreatedAt = now.AddMinutes(-25)
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                OrderId = order3.Id,
                Order = order3,
                ProductId = products["Flan"].Id,
                Product = products["Flan"],
                Quantity = 2,
                UnitPrice = products["Flan"].Price,
                CreatedAt = now.AddMinutes(-25)
            },
            // Order 4 lines
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                OrderId = order4.Id,
                Order = order4,
                ProductId = products["FishTacos"].Id,
                Product = products["FishTacos"],
                Quantity = 2,
                UnitPrice = products["FishTacos"].Price,
                CreatedAt = now.AddMinutes(-30)
            },
            new()
            {
                PublicId = PublicIdHelper.Generate(),
                OrderId = order4.Id,
                Order = order4,
                ProductId = products["ChickenSoup"].Id,
                Product = products["ChickenSoup"],
                Quantity = 1,
                UnitPrice = products["ChickenSoup"].Price,
                CreatedAt = now.AddMinutes(-30)
            }
        };

        _context.OrderLines.AddRange(orderLines);
        await _context.SaveChangesAsync();
    }
}







