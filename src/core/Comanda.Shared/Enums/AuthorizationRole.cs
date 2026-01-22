namespace Comanda.Shared.Enums;

public enum AuthorizationRole
{
    Orderer = 1,    // Can place orders on the account
    Payer = 2,      // Can make payments on the account
    Admin = 3,      // Can manage account settings
    Viewer = 4      // Can view account information only
}
