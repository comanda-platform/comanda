namespace Comanda.Client.Kitchen.Infrastructure.Localization;

using Comanda.Client.Kitchen.Infrastructure.Services;

public class LocalizationService
{
    private readonly ISecureStorageService _secureStorage;
    private string _currentLanguage = "en";
    
    private static readonly Dictionary<string, Dictionary<string, string>> _translations = new()
    {
        ["en"] = new Dictionary<string, string>
        {
            // Navigation
            ["Home"] = "Home",
            ["Production"] = "Production",
            ["Fulfillment"] = "Fulfillment",
            ["Kitchen"] = "Kitchen",
            
            // Common
            ["Loading"] = "Loading...",
            ["Refresh"] = "Refresh",
            ["Close"] = "Close",
            ["Cancel"] = "Cancel",
            ["Save"] = "Save",
            ["Start"] = "Start",
            ["Date"] = "Date",
            ["Orders"] = "orders",
            
            // Authentication
            ["SignOut"] = "Sign Out",
            ["Login"] = "Login",
            ["Email"] = "Email",
            ["Password"] = "Password",
            ["SignIn"] = "Sign In",
            ["InvalidCredentials"] = "Invalid email or password",
            ["Language"] = "Language",
            
            // Production
            ["ProductionBoard"] = "Production Board",
            ["StartCooking"] = "Start Cooking",
            ["EnterYield"] = "Enter Yield",
            ["MarkReady"] = "Mark Ready",
            ["ViewRecipe"] = "View Recipe",
            ["Cooking"] = "Cooking",
            ["CookingFor"] = "Cooking for:",
            ["BatchComplete"] = "Batch complete!",
            ["PortionsReady"] = "portions ready",
            ["ReadyToCook"] = "Ready to cook",
            ["NoMenuItems"] = "No menu items found for",
            ["LoadingMenu"] = "Loading menu...",
            ["HowManyPortions"] = "How many portions were produced in this batch?",
            ["Portions"] = "Portions",
            ["PrefilledFromRecipe"] = "Pre-filled from recipe estimate. Adjust if needed.",
            ["BatchesCompleted"] = "batch(es) completed",
            
            // Availability
            ["Produced"] = "Produced",
            ["Committed"] = "Committed",
            ["Remaining"] = "Remaining",
            ["Good"] = "Good",
            ["Low"] = "Low",
            ["Critical"] = "Critical",
            ["SoldOut"] = "Sold Out",
            
            // Recipe
            ["Recipe"] = "Recipe",
            ["Ingredients"] = "Ingredients",
            ["Ingredient"] = "Ingredient",
            ["Quantity"] = "Quantity",
            ["Unit"] = "Unit",
            ["EstimatedPortions"] = "Estimated Portions (Recipe)",
            ["CalculateFor"] = "Calculate for",
            ["QuantitiesScaled"] = "Quantities scaled from {0} to {1} portions",
            ["ScaledFrom"] = "Scaled from {0}",
            ["NoIngredientsListed"] = "No ingredients listed for this recipe.",
            ["NoRecipeAvailable"] = "No recipe available.",
            
            // Fulfillment
            ["FulfillmentQueue"] = "Fulfillment Queue",
            ["Order"] = "Order",
            ["AcceptOrder"] = "Accept Order",
            ["StartPreparing"] = "Start Preparing",
            ["Plate"] = "Plate",
            ["PlateAllItemsFirst"] = "Plate all items first",
            ["NoActiveOrders"] = "No active orders in the queue.",
            ["OrdersWillAppear"] = "Orders will appear here automatically.",
            ["OrderAccepted"] = "Order accepted!",
            ["StartedPreparing"] = "Started preparing!",
            ["OrderReady"] = "Order ready for pickup!",
            ["PrepStarted"] = "Prep started!",
            ["ItemPlated"] = "Item plated!",
            ["Plating"] = "Plating",
            ["ReviewPlating"] = "Review plating",
            ["Container"] = "Container",
            ["Sides"] = "Sides",
            ["NoSides"] = "No sides",
            ["CompletePlating"] = "Complete Plating",
            
            // Pull to refresh
            ["PullToRefresh"] = "Pull to refresh",
            ["ReleaseToRefresh"] = "Release to refresh",
            ["Refreshing"] = "Refreshing...",
            
            // Errors
            ["ErrorOccurred"] = "An error occurred",
            ["ErrorLoadingMenu"] = "Error loading menu:",
            ["ErrorRefreshingOrders"] = "Error refreshing orders:",
            ["FailedToAcceptOrder"] = "Failed to accept order.",
            ["FailedToStartPreparing"] = "Failed to start preparing.",
            ["FailedToMarkReady"] = "Failed to mark ready.",
            ["NoRecipeFound"] = "No recipe found for this item.",
            ["UnableToConnect"] = "Unable to connect to the server. Please check your connection.",

            // Fulfillment Types
            ["DineIn"] = "Dine In",
            ["TakeAway"] = "Take Away",
            ["Delivery"] = "Delivery",
            ["Unknown"] = "Unknown",

            // Login
            ["WelcomeToKitchen"] = "Welcome to the Kitchen!",
            ["SigningIn"] = "Signing in...",
            ["Item"] = "Item"
        },
        
        ["es"] = new Dictionary<string, string>
        {
            // Navigation
            ["Home"] = "Inicio",
            ["Production"] = "Producción",
            ["Fulfillment"] = "Preparación",
            ["Kitchen"] = "Cocina",
            
            // Common
            ["Loading"] = "Cargando...",
            ["Refresh"] = "Actualizar",
            ["Close"] = "Cerrar",
            ["Cancel"] = "Cancelar",
            ["Save"] = "Guardar",
            ["Start"] = "Iniciar",
            ["Date"] = "Fecha",
            ["Orders"] = "pedidos",
            
            // Authentication
            ["SignOut"] = "Cerrar Sesión",
            ["Login"] = "Iniciar Sesión",
            ["Email"] = "Correo",
            ["Password"] = "Contraseña",
            ["SignIn"] = "Entrar",
            ["InvalidCredentials"] = "Correo o contraseña inválidos",
            ["Language"] = "Idioma",
            
            // Production
            ["ProductionBoard"] = "Tablero de Producción",
            ["StartCooking"] = "Iniciar Cocción",
            ["EnterYield"] = "Ingresar Rendimiento",
            ["MarkReady"] = "Marcar Listo",
            ["ViewRecipe"] = "Ver Receta",
            ["Cooking"] = "Cocinando",
            ["CookingFor"] = "Cocinando por:",
            ["BatchComplete"] = "¡Lote completado!",
            ["PortionsReady"] = "porciones listas",
            ["ReadyToCook"] = "Listo para cocinar",
            ["NoMenuItems"] = "No se encontraron elementos del menú para",
            ["LoadingMenu"] = "Cargando menú...",
            ["HowManyPortions"] = "¿Cuántas porciones se produjeron en este lote?",
            ["Portions"] = "Porciones",
            ["PrefilledFromRecipe"] = "Pre-llenado desde el estimado de la receta. Ajuste si es necesario.",
            ["BatchesCompleted"] = "lote(s) completado(s)",
            
            // Availability
            ["Produced"] = "Producido",
            ["Committed"] = "Comprometido",
            ["Remaining"] = "Disponible",
            ["Good"] = "Bien",
            ["Low"] = "Bajo",
            ["Critical"] = "Crítico",
            ["SoldOut"] = "Agotado",
            
            // Recipe
            ["Recipe"] = "Receta",
            ["Ingredients"] = "Ingredientes",
            ["Ingredient"] = "Ingrediente",
            ["Quantity"] = "Cantidad",
            ["Unit"] = "Unidad",
            ["EstimatedPortions"] = "Porciones Estimadas (Receta)",
            ["CalculateFor"] = "Calcular para",
            ["QuantitiesScaled"] = "Cantidades escaladas de {0} a {1} porciones",
            ["ScaledFrom"] = "Escalado de {0}",
            ["NoIngredientsListed"] = "No hay ingredientes listados para esta receta.",
            ["NoRecipeAvailable"] = "No hay receta disponible.",
            
            // Fulfillment
            ["FulfillmentQueue"] = "Cola de Preparación",
            ["Order"] = "Pedido",
            ["AcceptOrder"] = "Aceptar Pedido",
            ["StartPreparing"] = "Iniciar Preparación",
            ["Plate"] = "Emplatar",
            ["PlateAllItemsFirst"] = "Emplate todos los artículos primero",
            ["NoActiveOrders"] = "No hay pedidos activos en la cola.",
            ["OrdersWillAppear"] = "Los pedidos aparecerán aquí automáticamente.",
            ["OrderAccepted"] = "¡Pedido aceptado!",
            ["StartedPreparing"] = "¡Preparación iniciada!",
            ["OrderReady"] = "¡Pedido listo para entrega!",
            ["PrepStarted"] = "¡Preparación iniciada!",
            ["ItemPlated"] = "¡Artículo emplatado!",
            ["Plating"] = "Emplatado",
            ["ReviewPlating"] = "Revisar emplatado",
            ["Container"] = "Contenedor",
            ["Sides"] = "Acompañamientos",
            ["NoSides"] = "Sin acompañamientos",
            ["CompletePlating"] = "Completar Emplatado",
            
            // Pull to refresh
            ["PullToRefresh"] = "Desliza para actualizar",
            ["ReleaseToRefresh"] = "Suelta para actualizar",
            ["Refreshing"] = "Actualizando...",
            
            // Errors
            ["ErrorOccurred"] = "Ocurrió un error",
            ["ErrorLoadingMenu"] = "Error al cargar el menú:",
            ["ErrorRefreshingOrders"] = "Error al actualizar pedidos:",
            ["FailedToAcceptOrder"] = "Error al aceptar el pedido.",
            ["FailedToStartPreparing"] = "Error al iniciar la preparación.",
            ["FailedToMarkReady"] = "Error al marcar como listo.",
            ["NoRecipeFound"] = "No se encontró receta para este artículo.",
            ["UnableToConnect"] = "No se puede conectar al servidor. Por favor verifique su conexión.",

            // Fulfillment Types
            ["DineIn"] = "En Mesa",
            ["TakeAway"] = "Para Llevar",
            ["Delivery"] = "Entrega",
            ["Unknown"] = "Desconocido",

            // Login
            ["WelcomeToKitchen"] = "¡Bienvenido a la Cocina!",
            ["SigningIn"] = "Iniciando sesión...",
            ["Item"] = "Artículo"
        }
    };
    
    public event Action? OnLanguageChanged;
    
    public LocalizationService(ISecureStorageService secureStorage)
    {
        _secureStorage = secureStorage;
        AppStrings.SetLocalizer(GetString);
    }
    
    public string CurrentLanguage => _currentLanguage;
    
    public IReadOnlyList<(string Code, string DisplayName)> AvailableLanguages => new List<(string, string)>
    {
        ("en", "English"),
        ("es", "Español")
    };
    
    public async Task InitializeAsync()
    {
        var savedLanguage = await _secureStorage.GetAsync("app_language");
        if (!string.IsNullOrEmpty(savedLanguage) && _translations.ContainsKey(savedLanguage))
        {
            _currentLanguage = savedLanguage;
        }
    }
    
    public async Task SetLanguageAsync(string languageCode)
    {
        if (!_translations.ContainsKey(languageCode))
            return;
            
        _currentLanguage = languageCode;
        await _secureStorage.SetAsync("app_language", languageCode);
        OnLanguageChanged?.Invoke();
    }
    
    public string GetString(string key)
    {
        if (_translations.TryGetValue(_currentLanguage, out var languageDict))
        {
            if (languageDict.TryGetValue(key, out var translation))
                return translation;
        }
        
        // Fallback to English
        if (_translations.TryGetValue("en", out var englishDict))
        {
            if (englishDict.TryGetValue(key, out var translation))
                return translation;
        }
        
        // Return key if no translation found
        return key;
    }
    
    public string GetString(string key, params object[] args)
    {
        var template = GetString(key);
        return string.Format(template, args);
    }
    
    public string this[string key] => GetString(key);
}







