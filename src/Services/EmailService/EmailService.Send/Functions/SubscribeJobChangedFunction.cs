using Azure.Messaging.ServiceBus;
using EmailService.Send.Commands.ContactPersonReceiver;
using EmailService.Send.Constants;
using EmailService.Send.Events;
using EmailService.Send.Events.Jobs;
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
    public class SubscribeJobChangedFunction : SendBaseFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SubscribeJobChangedFunction> _logger;

        public SubscribeJobChangedFunction(IMediator mediator, ILogger<SubscribeJobChangedFunction> log)
        {
            _mediator = mediator;
            _logger = log;
        }

        [FunctionName(nameof(SubscribeJobChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                        Topics.JobChanged.Name,
                        Topics.JobChanged.Subscribers.EmailService,
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
                        await SendJobApprovedAsync(message, messageActions, cancellationToken);
                        break;
                    case Topics.JobChanged.Filters.JobRejected:
                        await SendJobRejectedAsync(message, messageActions, cancellationToken);
                        break;
                    case Topics.JobChanged.Filters.JobInitialized:
                        await SendJobSubmittedAsync(message, messageActions, cancellationToken);
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

        private async Task SendJobApprovedAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<JobChangedEvent, JobChangedPayload>(message, messageActions, cancellationToken);

            if (payload.Company != null)
            {
                await Parallel.ForEachAsync(payload.Company.ContactPersons, cancellationToken, async (contactPerson, _) =>
                {
                    try
                    {
                        var sendJobApprovedCommand = new SendJobApprovedCommand(
                            message.Subject,
                            payload.JobId,
                            payload.Position?.Code,
                            payload.Company.Name,
                            contactPerson.PersonId,
                            contactPerson.FirstName,
                            contactPerson.Email,
                            contactPerson.SystemLanguage);
                        await _mediator.Publish(sendJobApprovedCommand, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(
                            ex,
                            "Error occurred in SubscribeJobChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}. " +
                            " Failed to send job approved email. ContactPersonId: {ContactPersonId}",
                            message.MessageId,
                            message.CorrelationId,
                            payload,
                            contactPerson.PersonId);
                    }
                });
            }
        }

        private async Task SendJobRejectedAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<JobChangedEvent, JobChangedPayload>(message, messageActions, cancellationToken);

            if (payload.Company != null)
            {
                await Parallel.ForEachAsync(payload.Company.ContactPersons, cancellationToken, async (contactPerson, _) =>
                {
                    try
                    {
                        var sendJobRejectedCommand = new SendJobRejectedCommand(
                            message.Subject,
                            payload.JobId,
                            payload.Position?.Code,
                            contactPerson.PersonId,
                            contactPerson.FirstName,
                            contactPerson.Email,
                            contactPerson.SystemLanguage);
                        await _mediator.Publish(sendJobRejectedCommand, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(
                            ex,
                            "Error occurred in SubscribeJobChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}. " +
                            " Failed to send job rejected email. ContactPersonId: {ContactPersonId}",
                            message.MessageId,
                            message.CorrelationId,
                            payload,
                            contactPerson.PersonId);
                    }
                });
            }
        }

        private async Task SendJobSubmittedAsync(
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken cancellationToken)
        {
            var payload = await GetPayloadAsync<JobChangedEvent, JobChangedPayload>(message, messageActions, cancellationToken);

            if (payload.Company != null)
            {
                await Parallel.ForEachAsync(payload.Company.ContactPersons, cancellationToken, async (contactPerson, _) =>
                {
                    try
                    {
                        var sendJobSubmittedCommand = new SendJobSubmittedCommand(
                            message.Subject,
                            payload.JobId,
                            payload.Position?.Code,
                            contactPerson.PersonId,
                            contactPerson.FirstName,
                            contactPerson.Email,
                            contactPerson.SystemLanguage);
                        await _mediator.Publish(sendJobSubmittedCommand, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(
                            ex,
                            "Error occurred in SubscribeJobChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}. " +
                            " Failed to send job submitted email. ContactPersonId: {ContactPersonId}",
                            message.MessageId,
                            message.CorrelationId,
                            payload,
                            contactPerson.PersonId);
                    }
                });
            }
        }
    }
}
