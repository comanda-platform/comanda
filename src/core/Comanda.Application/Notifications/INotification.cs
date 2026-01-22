namespace Comanda.Application.Notifications;

public interface INotification
{
    string Name { get; }
    object Payload { get; }
}







