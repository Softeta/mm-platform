namespace EventBus.EventHandlers
{
    public abstract class SubscribersEventHandlersManager : ISubscribersEventHandlersManager
    {
        private readonly Dictionary<string, IntegrationEventHandler> _subscribers = new();

        public IntegrationEventHandler? GetHandler(string filterName)
        {
            return _subscribers.TryGetValue(filterName, out var handler) ? handler : null;
        }

        protected void AddSubscriberHandler(string filterName, IntegrationEventHandler? handler)
        {
            if (handler is null)
            {
                return;
            }

            if (!_subscribers.ContainsKey(filterName))
            {
                _subscribers.Add(filterName, handler);
                return;
            }

            throw new Exception($"Handler already added for filter {filterName}");
        }
    }
}
