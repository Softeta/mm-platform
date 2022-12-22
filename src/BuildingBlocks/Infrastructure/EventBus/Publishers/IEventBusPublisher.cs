using EventBus.EventHandlers;

namespace EventBus.Publishers
{
    public interface IEventBusPublisher
    {
        Task PublishAsync<T>(T integrationEvent, string filterName, CancellationToken cancellationToken) where T : IntegrationEvent;
        Task PublishAsync<T>(IEnumerable<T> integrationEvents, string filterName, CancellationToken cancellationToken) where T : IntegrationEvent;
    }
}
