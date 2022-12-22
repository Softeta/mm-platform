namespace EventBus.Subscribers.Interfaces
{
    public interface IEventBusSubscriber
    {
        Task RegisterSubscriptionClientAsync();
        Task UnRegisterSubscriptionClientAsync();
        Task CloseAsync();
    }
}
