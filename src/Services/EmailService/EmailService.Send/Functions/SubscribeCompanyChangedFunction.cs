using Azure.Messaging.ServiceBus;
using EmailService.Send.Commands.ContactPersonReceiver;
using EmailService.Send.Constants;
using EmailService.Send.Events;
using EmailService.Send.Events.Companies;
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
    public class SubscribeCompanyChangedFunction : SendBaseFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SubscribeCompanyChangedFunction> _logger;

        public SubscribeCompanyChangedFunction(IMediator mediator, ILogger<SubscribeCompanyChangedFunction> log)
        {
            _mediator = mediator;
            _logger = log;
        }

        [FunctionName(nameof(SubscribeCompanyChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                Topics.CompanyChanged.Name,
                Topics.CompanyChanged.Subscribers.EmailService,
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
                    case Topics.CompanyChanged.Filters.CompanyApproved:
                        await SendContactPersonWelcomeAsync(message, messageActions, cancellationToken);
                        break;
                    case Topics.CompanyChanged.Filters.CompanyRejected:
                        await SendCompanyRejectedAsync(message, messageActions, cancellationToken);
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
                    "Error occurred in SubscribeCompanyChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                throw;
            }
        }

        private async Task SendContactPersonWelcomeAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<CompanyChangedEvent, CompanyChangedPayload>(message, messageActions, cancellationToken);

            await Parallel.ForEachAsync(payload.ContactPersons, cancellationToken, async (contactPerson, _) =>
            {
                var sendWelcomeEmailCommand = new SendContactPersonWelcomeCommand(
                    message.Subject,
                    contactPerson.Id,
                    contactPerson.Email.Address,
                    contactPerson.SystemLanguage,
                    contactPerson.FirstName,
                    contactPerson.CompanyName);

                await _mediator.Publish(sendWelcomeEmailCommand, cancellationToken);
            }); 
        }

        private async Task SendCompanyRejectedAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<CompanyChangedEvent, CompanyChangedPayload>(message, messageActions, cancellationToken);

            await Parallel.ForEachAsync(payload.ContactPersons, cancellationToken, async (contactPerson, _) =>
            {
                var sendRejectedEmailCommand = new SendCompanyRejectedCommand(
                    message.Subject,
                    contactPerson.Id,
                    contactPerson.Email.Address,
                    contactPerson.SystemLanguage,
                    contactPerson.FirstName,
                    contactPerson.CompanyName);

                await _mediator.Publish(sendRejectedEmailCommand, cancellationToken);
            });
        }
    }
}
