using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using ElasticSearch.Shared.Constants;
using ElasticSearch.Sync.Commands;
using ElasticSearch.Sync.Events;
using ElasticSearch.Sync.Events.Models.CandidateJobsChanged;
using EventBus.Constants;
using EventBus.Events;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace ElasticSearch.Sync.Functions
{
    public class SubscribeCandidateJobsChangedFunction
    {
        private readonly ILogger<SubscribeCandidateJobsChangedFunction> _logger;
        private readonly IMediator _mediator;

        public SubscribeCandidateJobsChangedFunction(ILogger<SubscribeCandidateJobsChangedFunction> log, IMediator mediator)
        {
            _logger = log;
            _mediator = mediator;
        }

        [FunctionName(nameof(SubscribeCandidateJobsChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                Topics.CandidateJobsChanged.Name,
                Topics.CandidateJobsChanged.Subscribers.ElasticSearch,
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
                    case Topics.CandidateJobsChanged.Filters.CandidateJobsAdded:
                    case Topics.CandidateJobsChanged.Filters.CandidateJobsUpdated:
                    case Topics.CandidateJobsChanged.Filters.CandidateJobsArchived:
                    case Topics.CandidateJobsChanged.Filters.CandidateJobsHired:
                    case Topics.CandidateJobsChanged.Filters.CandidateJobRejected:
                        var @event = await EventParser.ExecuteAsync<CandidateJobsChangedEvent, CandidateJobsChangedPayload>
                            (message, messageActions, cancellationToken);
                        await _mediator.Publish(new SyncCandidateJobsDocumentCommand(message.Subject, @event), cancellationToken);
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
                    "Error occurred in SubscribeCandidateJobsChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                throw;
            }
        }
    }
}
