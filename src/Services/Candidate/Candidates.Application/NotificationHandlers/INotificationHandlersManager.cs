namespace Candidates.Application.NotificationHandlers
{
    public interface INotificationHandlersManager
    {
        INotificationHandler GetHandler(string notificationType);
    }
}
