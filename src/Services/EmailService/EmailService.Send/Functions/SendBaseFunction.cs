using Azure.Messaging.ServiceBus;
using EventBus.Events;
using Microsoft.Azure.WebJobs.ServiceBus;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Functions
{
    public abstract class SendBaseFunction
    {
        protected static async Task<TPayload> GetPayloadAsync<TEvent, TPayload>(
           ServiceBusReceivedMessage message,
           ServiceBusMessageActions messageActions,
           CancellationToken cancellationToken)
               where TEvent : Event<TPayload>
               where TPayload : class
        {
            var @event = await EventParser.ExecuteAsync<TEvent, TPayload>(message, messageActions, cancellationToken);

            if (@event.Payload is null)
            {
                throw new InvalidDataContractException();
            }

            return @event.Payload;
        }
    }
}
