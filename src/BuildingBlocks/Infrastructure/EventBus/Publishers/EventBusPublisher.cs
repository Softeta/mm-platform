using Azure.Messaging.ServiceBus;
using EventBus.EventHandlers;
using Newtonsoft.Json;

namespace EventBus.Publishers
{
    public abstract class EventBusPublisher : IEventBusPublisher
    {
        private readonly ServiceBusSender _serviceBusSender;

        protected EventBusPublisher(ServiceBusClient serviceBusClient, string topicName)
        {
            _serviceBusSender = serviceBusClient.CreateSender(topicName);
        }

        public virtual async Task PublishAsync<T>(T integrationEvent, string filterName, CancellationToken cancellationToken) where T : IntegrationEvent
        {
            var message = new ServiceBusMessage(JsonConvert.SerializeObject(integrationEvent))
            {
                Subject = filterName
            };

            await _serviceBusSender.SendMessageAsync(message, cancellationToken);
        }

        public async Task PublishAsync<T>(IEnumerable<T> integrationEvents, string filterName, CancellationToken cancellationToken) where T : IntegrationEvent
        {
            var messages = integrationEvents
                .Select(x => new ServiceBusMessage(JsonConvert.SerializeObject(x))
                {
                    Subject = filterName
                });

            await _serviceBusSender.SendMessagesAsync(messages, cancellationToken);
        }
    }
}
