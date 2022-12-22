using Azure.Messaging.ServiceBus;
using EmailService.Send.Commands.JobReceiver;
using EmailService.Send.Constants;
using EmailService.Send.Events;
using EmailService.Send.Events.JobShare;
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

namespace EmailService.Send.Functions
{
    public class SendJobShareChangedFunction : SendBaseFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SendJobShareChangedFunction> _logger;

        public SendJobShareChangedFunction(IMediator mediator, ILogger<SendJobShareChangedFunction> log)
        {
            _mediator = mediator;
            _logger = log;
        }

        [FunctionName(nameof(SendJobShareChangedFunction))]
        public async Task Run(
            [ServiceBusTrigger(
                Topics.JobShareChanged.Name,
                Topics.JobShareChanged.Subscribers.EmailService,
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
                    case Topics.JobShareChanged.Filters.AskedForJobApproval:
                        var @event = await EventParser.ExecuteAsync<JobShareChangedEvent, JobShareChangedPayload>
                            (message, messageActions, cancellationToken);
                        var sendAskedForJobApproval = new SendAskedForJobApprovalCommand(filterName, @event);
                        await _mediator.Publish(sendAskedForJobApproval, cancellationToken);
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
                    "Error occurred in SendJobShareChangedFunction. MessageId:{MessageId}; CorrelationId: {CorrelationId};Payload:{Payload}",
                    message.MessageId,
                    message.CorrelationId,
                    payload);
                throw;
            }
        }
    }
}
