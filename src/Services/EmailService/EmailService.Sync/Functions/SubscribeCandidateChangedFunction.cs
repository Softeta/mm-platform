using Azure.Messaging.ServiceBus;
using EmailService.Sync.Constants;
using EmailService.Sync.Events;
using EmailService.Sync.Events.Candidate;
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
    public class SubscribeCandidateChangedFunction : BaseFunction
    {
        private readonly ILogger<SubscribeCandidateChangedFunction> _logger;
        private readonly ICandidateContactsClient _contactsClient;

        public SubscribeCandidateChangedFunction(ILogger<SubscribeCandidateChangedFunction> log, ICandidateContactsClient contactsClient)
        {
            _logger = log;
            _contactsClient = contactsClient;
        }

        [FunctionName(nameof(SubscribeCandidateChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                Topics.CandidateChanged.Name,
                Topics.CandidateChanged.Subscribers.EmailServiceSync,
                Connection = KeyVaultSecretNames.ServiceBusConnectionString,
                AutoCompleteMessages = false)]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            try
            {
                var filterName = message.Subject;
                var payload = await GetPayloadAsync<CandidateChangedEvent, CandidatePayload>(message, messageActions, cancellationToken);
                switch (filterName)
                {
                    case Topics.CandidateChanged.Filters.CandidateCreated:
                        await _contactsClient.CreateContactAsync(payload);
                        break;
                    case Topics.CandidateChanged.Filters.CandidateUpdated:
                        var contact = await _contactsClient.GetContactAsync(payload.Email.Address);
                        if (contact == null)
                        {
                            await _contactsClient.CreateContactAsync(payload);
                        }
                        else
                        {
                            await _contactsClient.UpdateContactAsync(payload);
                        }
                        break;
                    case Topics.CandidateChanged.Filters.CandidateRejected:
                        await _contactsClient.DeleteContactAsync(payload.Email.Address);
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
                    "Error occurred in SubscribeCandidateChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                throw;
            }
        }
    }
}
