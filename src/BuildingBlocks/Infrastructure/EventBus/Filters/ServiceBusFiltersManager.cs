using Azure.Messaging.ServiceBus.Administration;
using System.Collections.ObjectModel;

namespace EventBus.Filters
{
    public class ServiceBusFiltersManager : IServiceBusFiltersManager
    {
        private readonly ServiceBusAdministrationClient _adminClient;

        public ServiceBusFiltersManager(ServiceBusAdministrationClient adminClient)
        {
            _adminClient = adminClient;
        }

        public async Task CreateFiltersAsync(string topicName, string subscriptionName, string[] filterNames)
        {
            var rules = _adminClient.GetRulesAsync(topicName, subscriptionName);
            var correlationFilters = new Collection<CorrelationRuleFilter>();

            await foreach (var rule in rules)
            {
                if (rule.Filter is CorrelationRuleFilter filter)
                {
                    correlationFilters.Add(filter);
                }
            }

            var ruleTasks = new Collection<Task>();

            foreach (var filterName in filterNames)
            {
                var doFilterAlreadyExist = correlationFilters.Any(f => f.Subject == filterName);

                if (doFilterAlreadyExist)
                {
                    continue;
                }

                ruleTasks.Add(_adminClient.CreateRuleAsync(
                    topicName,
                    subscriptionName,
                    new CreateRuleOptions
                    {
                        Filter = new CorrelationRuleFilter { Subject = filterName },
                        Name = filterName
                    }));
            }

            await Task.WhenAll(ruleTasks);
        }
    }
}
