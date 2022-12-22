using Azure.Messaging.ServiceBus;
using EmailService.Send.Commands.CandidateReceiver;
using EmailService.Send.Constants;
using EmailService.Send.Events;
using EmailService.Send.Events.Candidates;
using EventBus.Constants;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Functions
{
    public class SendCandidateChangedFunction : SendBaseFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SendJobShareChangedFunction> _logger;

        public SendCandidateChangedFunction(IMediator mediator, ILogger<SendJobShareChangedFunction> log)
        {
            _mediator = mediator;
            _logger = log;
        }

        [FunctionName(nameof(SendCandidateChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                        Topics.CandidateChanged.Name,
                        Topics.CandidateChanged.Subscribers.EmailService,
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
                    case Topics.CandidateChanged.Filters.CandidateRegistered:
                    case Topics.CandidateChanged.Filters.CandidateEmailVerificationRequested:
                        await SendEmailVerificationAsync(message, messageActions, cancellationToken);
                        break;
                    case Topics.CandidateChanged.Filters.CandidateApproved:
                        await SendCandidateWelcomeAsync(message, messageActions, cancellationToken);
                        break;
                    case Topics.CandidateChanged.Filters.CandidateRejected:
                        await SendCandidateRejectedAsync(message, messageActions, cancellationToken);
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
                    "Error occurred in SendCandidateChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                throw;
            }
        }

        private async Task SendEmailVerificationAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<CandidateChangedEvent, CandidateChangedPayload>(
                message, messageActions, cancellationToken);

            var sendEmailVerification = new SendCandidateVerificationCommand(
                message.Subject,
                payload.Id,
                payload.Email?.Address,
                payload.Email?.VerificationKey,
                payload.SystemLanguage);

            await _mediator.Publish(sendEmailVerification, cancellationToken);
        }

        private async Task SendCandidateWelcomeAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<CandidateChangedEvent, CandidateChangedPayload>(
                message, messageActions, cancellationToken);

            var sendCandidateApprovedCommand = new SendCandidateWelcomeCommand(message.Subject,
                payload.Id,
                payload.Email?.Address,
                payload.FirstName,
                payload.SystemLanguage);
            await _mediator.Publish(sendCandidateApprovedCommand, cancellationToken);
        }

        private async Task SendCandidateRejectedAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<CandidateChangedEvent, CandidateChangedPayload>(
                message, messageActions, cancellationToken);

            var sendCandidateRejectedCommand = new SendCandidateRejectedCommand(message.Subject,
                payload.Id,
                payload.Email?.Address,
                payload.SystemLanguage);
            await _mediator.Publish(sendCandidateRejectedCommand, cancellationToken);
        }
    }
}
