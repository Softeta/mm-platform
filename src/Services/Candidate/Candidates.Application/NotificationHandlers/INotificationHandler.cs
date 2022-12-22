namespace Candidates.Application.NotificationHandlers
{
    public interface INotificationHandler
    {
        public string NotificationType { get; }
        Task ExecuteAsync(Guid candidateId, Guid externalId, string notification);
    }
}
