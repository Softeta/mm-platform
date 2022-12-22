namespace EventBus.EventHandlers;

public interface ISubscribersEventHandlersManager
{
    IntegrationEventHandler? GetHandler(string filterName);
}