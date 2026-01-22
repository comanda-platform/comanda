namespace Comanda.Client.Kitchen.Infrastructure.Localization;

/// <summary>
/// Application string resources for localization
/// </summary>
public static class AppStrings
{
    // Navigation
    public static string Home => L("Home");
    public static string Production => L("Production");
    public static string Fulfillment => L("Fulfillment");
    public static string Kitchen => L("Kitchen");

    // Common
    public static string Loading => L("Loading");
    public static string Refresh => L("Refresh");
    public static string Close => L("Close");
    public static string Cancel => L("Cancel");
    public static string Save => L("Save");
    public static string Start => L("Start");
    public static string Date => L("Date");
    public static string Orders => L("Orders");

    // Authentication
    public static string SignOut => L("SignOut");
    public static string Login => L("Login");
    public static string Email => L("Email");
    public static string Password => L("Password");
    public static string SignIn => L("SignIn");
    public static string InvalidCredentials => L("InvalidCredentials");
    public static string Language => L("Language");

    // Production
    public static string ProductionBoard => L("ProductionBoard");
    public static string StartCooking => L("StartCooking");
    public static string EnterYield => L("EnterYield");
    public static string MarkReady => L("MarkReady");
    public static string ViewRecipe => L("ViewRecipe");
    public static string Cooking => L("Cooking");
    public static string CookingFor => L("CookingFor");
    public static string BatchComplete => L("BatchComplete");
    public static string PortionsReady => L("PortionsReady");
    public static string ReadyToCook => L("ReadyToCook");
    public static string NoMenuItems => L("NoMenuItems");
    public static string LoadingMenu => L("LoadingMenu");
    public static string HowManyPortions => L("HowManyPortions");
    public static string Portions => L("Portions");
    public static string PrefilledFromRecipe => L("PrefilledFromRecipe");
    public static string BatchesCompleted => L("BatchesCompleted");

    // Availability
    public static string Produced => L("Produced");
    public static string Committed => L("Committed");
    public static string Remaining => L("Remaining");
    public static string Good => L("Good");
    public static string Low => L("Low");
    public static string Critical => L("Critical");
    public static string SoldOut => L("SoldOut");

    // Recipe
    public static string Recipe => L("Recipe");
    public static string Ingredients => L("Ingredients");
    public static string Ingredient => L("Ingredient");
    public static string Quantity => L("Quantity");
    public static string Unit => L("Unit");
    public static string EstimatedPortions => L("EstimatedPortions");
    public static string CalculateFor => L("CalculateFor");
    public static string QuantitiesScaled => L("QuantitiesScaled");
    public static string NoIngredientsListed => L("NoIngredientsListed");
    public static string NoRecipeAvailable => L("NoRecipeAvailable");

    // Fulfillment
    public static string FulfillmentQueue => L("FulfillmentQueue");
    public static string Order => L("Order");
    public static string AcceptOrder => L("AcceptOrder");
    public static string StartPreparing => L("StartPreparing");
    public static string Plate => L("Plate");
    public static string PlateAllItemsFirst => L("PlateAllItemsFirst");
    public static string NoActiveOrders => L("NoActiveOrders");
    public static string OrdersWillAppear => L("OrdersWillAppear");
    public static string OrderAccepted => L("OrderAccepted");
    public static string StartedPreparing => L("StartedPreparing");
    public static string OrderReady => L("OrderReady");
    public static string PrepStarted => L("PrepStarted");
    public static string ItemPlated => L("ItemPlated");
    public static string Plating => L("Plating");

    // Pull to refresh
    public static string PullToRefresh => L("PullToRefresh");
    public static string ReleaseToRefresh => L("ReleaseToRefresh");
    public static string Refreshing => L("Refreshing");

    // Errors
    public static string ErrorOccurred => L("ErrorOccurred");
    public static string ErrorLoadingMenu => L("ErrorLoadingMenu");
    public static string ErrorRefreshingOrders => L("ErrorRefreshingOrders");
    public static string FailedToAcceptOrder => L("FailedToAcceptOrder");
    public static string FailedToStartPreparing => L("FailedToStartPreparing");
    public static string FailedToMarkReady => L("FailedToMarkReady");
    public static string NoRecipeFound => L("NoRecipeFound");

    // Helper to get localized string (will be set by LocalizationService)
    private static Func<string, string> L { get; set; } = key => key;

    public static void SetLocalizer(Func<string, string> localizer)
    {
        L = localizer;
    }
}







