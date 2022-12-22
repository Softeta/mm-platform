namespace EventBus.Filters
{
    public interface IServiceBusFiltersManager
    {
        Task CreateFiltersAsync(string topicName, string subscriptionName, string[] filterNames);
    }
}
