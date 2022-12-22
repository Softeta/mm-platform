using Azure.Messaging.ServiceBus;
using EmailService.Sync.Constants;
using EmailService.Sync.Events;
using EmailService.Sync.Events.ContactPerson;
using EmailService.Sync.SendInBlue;
using EventBus.Constants;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Sync.Functions
{
    public class SubscribeContactPersonChangedFunction : BaseFunction
    {
        private readonly ILogger<SubscribeContactPersonChangedFunction> _logger;
        private readonly IContactPersonContactsClient _contactPersonContactsClient;

        public SubscribeContactPersonChangedFunction(
            ILogger<SubscribeContactPersonChangedFunction> log, 
            IContactPersonContactsClient contactPersonContactsClient)
        {
            _logger = log;
            _contactPersonContactsClient = contactPersonContactsClient;
        }

        [FunctionName(nameof(SubscribeContactPersonChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                Topics.ContactPersonChanged.Name,
                Topics.ContactPersonChanged.Subscribers.EmailServiceSync,
                Connection = KeyVaultSecretNames.ServiceBusConnectionString,
                AutoCompleteMessages = false)]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            try
            {
                var filterName = message.Subject;
                var payload = await GetPayloadAsync<ContactPersonChangedEvent, ContactPersonPayload>(message, messageActions, cancellationToken);
                switch (filterName)
                {
                    case Topics.ContactPersonChanged.Filters.ContactPersonRegistered:
                    case Topics.ContactPersonChanged.Filters.ContactPersonLinked:
                        await _contactPersonContactsClient.CreateContactAsync(payload);
                        break;
                    case Topics.ContactPersonChanged.Filters.ContactPersonUpdated:
                        var contact = await _contactPersonContactsClient.GetContactAsync(payload.Email.Address);
                        if (contact == null)
                        {
                            await _contactPersonContactsClient.CreateContactAsync(payload);
                        }
                        else
                        {
                            await _contactPersonContactsClient.UpdateContactAsync(payload);
                        }
                        break;
                    case Topics.ContactPersonChanged.Filters.ContactPersonRejected:
                        await _contactPersonContactsClient.DeleteContactAsync(payload.Email.Address);
                        break;
                    default:
                        _logger.LogWarning(
                            "FilterName:{FilterName} is not supported. MessageId:{MessageId}; CorrelationId: {CorrelationId}",
                            filterName,
                            message.MessageId,
                            message.CorrelationId);
                        return;
                }

                await messageActions.CompleteMessageAsync(message, cancellationToken);
            }
            catch (Exception ex)
            {
                var payload = Encoding.UTF8.GetString(message.Body);

                _logger.LogCritical(
                    ex,
                    "Error occurred in SubscribeContactPersonChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                throw;
            }
        }
    }
}
