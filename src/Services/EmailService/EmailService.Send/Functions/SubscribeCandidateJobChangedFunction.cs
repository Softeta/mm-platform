using Azure.Messaging.ServiceBus;
using Domain.Seedwork.Exceptions;
using EmailService.Send.Commands.CandidateReceiver;
using EmailService.Send.Constants;
using EmailService.Send.Events;
using EmailService.Send.Events.CandidateJobs;
using EventBus.Constants;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Functions
{
    internal class SubscribeCandidateJobChangedFunction : SendBaseFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SubscribeCandidateJobChangedFunction> _logger;

        public SubscribeCandidateJobChangedFunction(IMediator mediator, ILogger<SubscribeCandidateJobChangedFunction> log)
        {
            _mediator = mediator;
            _logger = log;
        }

        [FunctionName(nameof(SubscribeCandidateJobChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                Topics.CandidateJobsChanged.Name,
                Topics.CandidateJobsChanged.Subscribers.EmailService,
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
                    case Topics.CandidateJobsChanged.Filters.CandidateAppliedToJob:
                        await SendCandidateAppliedToJobAsync(message, messageActions, cancellationToken);
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
                    "Error occurred in SubscribeCandidateJobChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                throw;
            }
        }

        private async Task SendCandidateAppliedToJobAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<CandidateJobChangedEvent, CandidateJobPayload>
                (message, messageActions, cancellationToken);

            var appliedToJob = payload.AppliedInJobs
                .Where(x => x.JobId == payload.JobId)
                .FirstOrDefault();

            if (appliedToJob is null)
            {
                throw new NotFoundException("Information of applied job not found");
            }

            if (IsCandidateInvited(payload)) return;

            var notification = new SendCandidateAppliedToJobCommand(
                message.Subject,
                appliedToJob,
                payload.Candidate?.FirstName,
                payload.Candidate?.Email,
                payload.Candidate?.SystemLanguage);

            await _mediator.Publish(notification);
        }

        private static bool IsCandidateInvited(CandidateJobPayload payload) => 
            payload.SelectedInJobs
                .Where(x => x.InvitedAt.HasValue)
                .Where(x => x.JobId == payload.JobId)
                .Any();
    }
}
