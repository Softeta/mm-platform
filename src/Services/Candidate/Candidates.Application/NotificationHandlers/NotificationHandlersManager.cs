namespace Candidates.Application.NotificationHandlers
{
    public class NotificationHandlersManager : INotificationHandlersManager
    {
        private readonly IEnumerable<INotificationHandler> _notificationHandlers;

        public NotificationHandlersManager(IEnumerable<INotificationHandler> notificationHandlers)
        {
            _notificationHandlers = notificationHandlers;
        }

        public INotificationHandler GetHandler(string notificationType)
        {
            var handler = _notificationHandlers.FirstOrDefault(x => x.NotificationType == notificationType);

            if (handler == null)
            {
                throw new ArgumentException($"Notification handler not registered for notification type {notificationType}");
            }

            return handler;
        }
    }
}
