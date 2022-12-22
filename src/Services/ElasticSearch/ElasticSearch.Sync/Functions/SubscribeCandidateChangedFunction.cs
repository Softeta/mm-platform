using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using ElasticSearch.Shared.Constants;
using ElasticSearch.Sync.Commands;
using ElasticSearch.Sync.Events;
using ElasticSearch.Sync.Events.Models.CandidateChanged;
using EventBus.Constants;
using EventBus.Events;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace ElasticSearch.Sync.Functions
{
    public class SubscribeCandidateChangedFunction
    {
        private readonly ILogger<SubscribeCandidateChangedFunction> _logger;
        private readonly IMediator _mediator;

        public SubscribeCandidateChangedFunction(ILogger<SubscribeCandidateChangedFunction> log, IMediator mediator)
        {
            _logger = log;
            _mediator = mediator;
        }

        [FunctionName(nameof(SubscribeCandidateChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                Topics.CandidateChanged.Name,
                Topics.CandidateChanged.Subscribers.ElasticSearch,
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
                    case Topics.CandidateChanged.Filters.CandidateCreated:
                    case Topics.CandidateChanged.Filters.CandidateUpdated:
                    case Topics.CandidateChanged.Filters.CandidateApproved:
                    case Topics.CandidateChanged.Filters.CandidateRejected:
                    case Topics.CandidateChanged.Filters.CandidateSkillsSynced:
                    case Topics.CandidateChanged.Filters.CandidateJobPositionSynced:
                        var @event = await EventParser.ExecuteAsync<CandidateChangedEvent, CandidateChangedPayload>
                            (message, messageActions, cancellationToken);
                        await _mediator.Publish(new SyncCandidateDocumentCommand(message.Subject, @event), cancellationToken);
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
