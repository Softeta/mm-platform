using Azure.Messaging.ServiceBus;
using Domain.Seedwork.Enums;
using EmailService.Send.Commands.ContactPersonReceiver;
using EmailService.Send.Constants;
using EmailService.Send.Events;
using EmailService.Send.Events.ContactPersons;
using EmailService.Send.Models.AppSettings;
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
    public class SubscribeContactPersonChangedFunction : SendBaseFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SubscribeContactPersonChangedFunction> _logger;

        public SubscribeContactPersonChangedFunction(IMediator mediator, ILogger<SubscribeContactPersonChangedFunction> log)
        {
            _mediator = mediator;
            _logger = log;
        }

        [FunctionName(nameof(SubscribeContactPersonChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                        Topics.ContactPersonChanged.Name,
                        Topics.ContactPersonChanged.Subscribers.EmailService,
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
                    case Topics.ContactPersonChanged.Filters.ContactPersonRegistered:
                    case Topics.ContactPersonChanged.Filters.ContactPersonEmailVerificationRequested:
                    case Topics.ContactPersonChanged.Filters.ContactPersonLinked:
                        await SendEmailVerificationAsync(message, messageActions, cancellationToken);
                        break;
                    case Topics.ContactPersonChanged.Filters.ContactPersonAdded:
                        await SendContactPersonInvitedAsync(message, messageActions, cancellationToken);
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

        private async Task SendEmailVerificationAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<ContactPersonChangedEvent, ContactPersonChangedPayload>(
                message, messageActions, cancellationToken);

            var sendEmailVerification = new SendContactPersonVerificationCommand(
                message.Subject,
                payload.CompanyId,
                payload.Id,
                payload.Email?.Address,
                payload?.Email?.VerificationKey,
                payload?.SystemLanguage);
            await _mediator.Publish(sendEmailVerification, cancellationToken);
        }

        private async Task SendContactPersonInvitedAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<ContactPersonChangedEvent, ContactPersonChangedPayload>(
                message, messageActions, cancellationToken);

            if (payload.CreatedBy is null)
            {
                _logger.LogCritical(
                    "Contact person created by was not found",
                    "Error occurred in SubscribeContactPersonChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                return;
            }

            if (payload.CreatedBy.Scope == Scope.BackOffice)
            {
                var sendWelcomeEmailCommand = new SendContactPersonInvitationCommand<ContactPersonInvitedBackOfficeOptions>(
                    message.Subject,
                    payload.Id,
                    payload.Email.Address,
                    payload.SystemLanguage,
                    payload.FirstName,
                    payload.CompanyName);
                await _mediator.Publish(sendWelcomeEmailCommand, cancellationToken);
            }
            if (payload.CreatedBy.Scope == Scope.Company)
            {
                var sendWelcomeEmailCommand = new SendContactPersonInvitationCommand<ContactPersonInvitedClientOptions>(
                    message.Subject,
                    payload.Id,
                    payload.Email.Address,
                    payload.SystemLanguage,
                    payload.FirstName,
                    payload.CompanyName);
                await _mediator.Publish(sendWelcomeEmailCommand, cancellationToken);
            }
        }
    }
}
