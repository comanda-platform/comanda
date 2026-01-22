namespace Comanda.Client.Delivery.Infrastructure.Localization;

public static class AppStrings
{
    private static Func<string, string>? _localizer;

    public static void SetLocalizer(Func<string, string> localizer)
    {
        _localizer = localizer;
    }

    public static string Get(string key)
    {
        return _localizer?.Invoke(key) ?? key;
    }
}







