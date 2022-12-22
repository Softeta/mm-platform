namespace EventBus.EventHandlers
{
    public interface IIntegrationEventHandler<in T> 
        where T : IntegrationEvent
    {
    }
}
