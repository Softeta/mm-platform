namespace EventBus.EventHandlers
{
    public abstract class IntegrationEventHandler
    {
        public abstract Task<bool> ExecuteAsync(string message);
    }
}
