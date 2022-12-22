using Azure.Messaging.ServiceBus;
using Domain.Seedwork.Enums;
using EmailService.Send.Commands.ContactPersonReceiver;
using EmailService.Send.Commands.JobCandidateReceiver;
using EmailService.Send.Constants;
using EmailService.Send.Events;
using EmailService.Send.Events.JobCandidates;
using EventBus.Constants;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Functions
{
    public class SubscribeJobCandidatesChangedFunction : SendBaseFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SubscribeJobCandidatesChangedFunction> _logger;

        public SubscribeJobCandidatesChangedFunction(IMediator mediator, ILogger<SubscribeJobCandidatesChangedFunction> log)
        {
            _mediator = mediator;
            _logger = log;
        }

        [FunctionName(nameof(SubscribeJobCandidatesChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                        Topics.JobCandidatesChanged.Name,
                        Topics.JobCandidatesChanged.Subscribers.EmailService,
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
                    case Topics.JobCandidatesChanged.Filters.JobSelectedCandidateInvited:
                        await SendInvitationEmailAsync(message, messageActions, cancellationToken);
                        break;
                    case Topics.JobCandidatesChanged.Filters.JobCandidatesSharedShortlistViaEmail:
                        await SendShortlistActivatedEmailAsync(message, messageActions, cancellationToken);
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
                    "Error occurred in SubscribeJobCandidateChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                throw;
            }
        }

        private async Task SendInvitationEmailAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<JobCandidatesChangedEvent, JobCandidateChangedPayload>(
                message, messageActions, cancellationToken);

            var sendEmailVerification = new SendJobCandidateInvitedCommand(message.Subject, payload);
            await _mediator.Publish(sendEmailVerification, cancellationToken);
        }

        private async Task SendShortlistActivatedEmailAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<JobCandidatesShortlistActivatedEvent, JobCandidatesShortlistActivatedPayload>(
                message, messageActions, cancellationToken);

            if (payload.JobCandidates is null)
            {
                throw new InvalidDataContractException();
            }

            var sendShortlistActivated = new SendShortlistActivatedCommand(
                message.Subject, 
                payload.JobCandidates.JobId, 
                payload.JobCandidates.Position?.Code,
                payload.ContactEmail,
                payload.ContactFirstName,
                payload.ContactSystemLanguage,
                payload.ContactExternalId.HasValue);
            await _mediator.Publish(sendShortlistActivated, cancellationToken);
        }
    }
}
