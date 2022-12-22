using Azure.Messaging.ServiceBus;
using ElasticSearch.Shared.Constants;
using ElasticSearch.Sync.Commands;
using ElasticSearch.Sync.Events;
using ElasticSearch.Sync.Events.Models.JobChanged;
using EventBus.Constants;
using EventBus.Events;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticSearch.Sync.Functions
{
    public class SubscribeJobChangedFunction
    {
        private readonly ILogger<SubscribeJobChangedFunction> _logger;
        private readonly IMediator _mediator;

        public SubscribeJobChangedFunction(ILogger<SubscribeJobChangedFunction> log, IMediator mediator)
        {
            _logger = log;
            _mediator = mediator;
        }

        [FunctionName(nameof(SubscribeJobChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                Topics.JobChanged.Name,
                Topics.JobChanged.Subscribers.ElasticSearch,
                Connection = KeyVaultSecretNames.ServiceBusConnectionString,
                AutoCompleteMessages = false)]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            try
            {
                var filterName = message.Subject;

                switch (filterName)
                {
                    case Topics.JobChanged.Filters.JobApproved:
                    case Topics.JobChanged.Filters.JobArchived:
                    case Topics.JobChanged.Filters.JobUpdated:
                    case Topics.JobChanged.Filters.JobPublished:
                    case Topics.JobChanged.Filters.JobSkillSynced:
                    case Topics.JobChanged.Filters.JobPositionSynced:
                        var @event = await EventParser.ExecuteAsync<JobChangedEvent, JobChangedPayload>
                            (message, messageActions, cancellationToken);
                        await _mediator.Publish(new SyncJobDocumentCommand(message.Subject, @event), cancellationToken);
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
                    "Error occurred in SubscribeJobChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                throw;
            }
        }
    }
}
