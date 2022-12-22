using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs.ServiceBus;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace EventBus.Events
{
    public static class EventParser
    {
        public static async Task<TE> ExecuteAsync<TE, TP>(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
            where TE : Event<TP>
            where TP : class
        {
            try
            {
                var payload = Encoding.UTF8.GetString(message.Body);
                var @event = JsonConvert.DeserializeObject<TE>(payload);

                if (@event?.Payload is null)
                {
                    throw new InvalidDataContractException();
                }

                return @event;
            }
            catch
            {
                await messageActions.CompleteMessageAsync(message, cancellationToken);
                throw;
            }
        }
    }
}
