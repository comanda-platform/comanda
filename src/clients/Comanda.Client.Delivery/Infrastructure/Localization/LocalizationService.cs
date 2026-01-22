namespace Comanda.Client.Delivery.Infrastructure.Localization;

using Comanda.Client.Delivery.Infrastructure.Services;

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
            ["Delivery"] = "Delivery",
            ["DeliveryQueue"] = "Delivery Queue",
            
            // Common
            ["Loading"] = "Loading...",
            ["Refresh"] = "Refresh",
            ["Close"] = "Close",
            ["Cancel"] = "Cancel",
            ["Save"] = "Save",
            ["Orders"] = "orders",
            
            // Authentication
            ["SignOut"] = "Sign Out",
            ["Login"] = "Login",
            ["Email"] = "Email",
            ["Password"] = "Password",
            ["SignIn"] = "Sign In",
            ["InvalidCredentials"] = "Invalid email or password",
            ["Language"] = "Language",
            
            // Delivery
            ["OrdersReadyForDelivery"] = "Orders Ready for Delivery",
            ["NoOrdersReady"] = "No orders ready for delivery.",
            ["OrdersWillAppear"] = "Orders will appear here when ready.",
            ["StartDelivery"] = "Start Delivery",
            ["MarkDelivered"] = "Mark Delivered",
            ["DeliveryStarted"] = "Delivery started!",
            ["OrderDelivered"] = "Order delivered!",
            ["FailedToStartDelivery"] = "Failed to start delivery.",
            ["FailedToMarkDelivered"] = "Failed to mark as delivered.",
            ["ViewOnMap"] = "View on Map",
            ["DeliveryLocation"] = "Delivery Location",
            ["AllDeliveryLocations"] = "All Delivery Locations",
            ["Items"] = "items",
            ["Order"] = "Order",
            ["NoLocationSet"] = "No location set",
            
            // Map
            ["Map"] = "Map",
            ["DeliveryMap"] = "Delivery Map",
            
            // Fulfillment Types
            ["DineIn"] = "Dine In",
            ["TakeAway"] = "Take Away",
            ["DeliveryType"] = "Delivery",
            ["Unknown"] = "Unknown",

            // Login
            ["WelcomeToDelivery"] = "Welcome to Delivery!",
            ["SigningIn"] = "Signing in...",
            
            // Errors
            ["ErrorOccurred"] = "An error occurred",
            ["ErrorRefreshingOrders"] = "Error refreshing orders:",
            ["UnableToConnect"] = "Unable to connect to the server. Please check your connection.",
            
            // Status
            ["Ready"] = "Ready",
            ["InTransit"] = "In Transit",
            ["Delivered"] = "Delivered"
        },
        
        ["es"] = new Dictionary<string, string>
        {
            // Navigation
            ["Home"] = "Inicio",
            ["Delivery"] = "Entregas",
            ["DeliveryQueue"] = "Cola de Entregas",
            
            // Common
            ["Loading"] = "Cargando...",
            ["Refresh"] = "Actualizar",
            ["Close"] = "Cerrar",
            ["Cancel"] = "Cancelar",
            ["Save"] = "Guardar",
            ["Orders"] = "pedidos",
            
            // Authentication
            ["SignOut"] = "Cerrar Sesión",
            ["Login"] = "Iniciar Sesión",
            ["Email"] = "Correo",
            ["Password"] = "Contraseña",
            ["SignIn"] = "Entrar",
            ["InvalidCredentials"] = "Correo o contraseña inválidos",
            ["Language"] = "Idioma",
            
            // Delivery
            ["OrdersReadyForDelivery"] = "Pedidos Listos para Entrega",
            ["NoOrdersReady"] = "No hay pedidos listos para entrega.",
            ["OrdersWillAppear"] = "Los pedidos aparecerán aquí cuando estén listos.",
            ["StartDelivery"] = "Iniciar Entrega",
            ["MarkDelivered"] = "Marcar Entregado",
            ["DeliveryStarted"] = "¡Entrega iniciada!",
            ["OrderDelivered"] = "¡Pedido entregado!",
            ["FailedToStartDelivery"] = "Error al iniciar la entrega.",
            ["FailedToMarkDelivered"] = "Error al marcar como entregado.",
            ["ViewOnMap"] = "Ver en Mapa",
            ["DeliveryLocation"] = "Ubicación de Entrega",
            ["AllDeliveryLocations"] = "Todas las Ubicaciones de Entrega",
            ["Items"] = "artículos",
            ["Order"] = "Pedido",
            ["NoLocationSet"] = "Sin ubicación",
            
            // Map
            ["Map"] = "Mapa",
            ["DeliveryMap"] = "Mapa de Entregas",
            
            // Fulfillment Types
            ["DineIn"] = "En Mesa",
            ["TakeAway"] = "Para Llevar",
            ["DeliveryType"] = "Entrega",
            ["Unknown"] = "Desconocido",

            // Login
            ["WelcomeToDelivery"] = "¡Bienvenido a Entregas!",
            ["SigningIn"] = "Iniciando sesión...",
            
            // Errors
            ["ErrorOccurred"] = "Ocurrió un error",
            ["ErrorRefreshingOrders"] = "Error al actualizar pedidos:",
            ["UnableToConnect"] = "No se puede conectar al servidor. Por favor verifique su conexión.",
            
            // Status
            ["Ready"] = "Listo",
            ["InTransit"] = "En Camino",
            ["Delivered"] = "Entregado"
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







