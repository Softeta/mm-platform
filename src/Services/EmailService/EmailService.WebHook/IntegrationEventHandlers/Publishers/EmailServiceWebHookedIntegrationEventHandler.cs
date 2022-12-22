using EmailService.WebHook.EventBus.Publishers;
using EmailService.WebHook.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.WebHook.IntegrationEventHandlers.Publishers
{
    public class EmailServiceWebHookedIntegrationEventHandler : INotificationHandler<EmailServiceWebHookedEvent>
    {
        private readonly IEmailServiceWebHookEventBusPublisher _eventBus;

        public EmailServiceWebHookedIntegrationEventHandler(IEmailServiceWebHookEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(EmailServiceWebHookedEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(EmailServiceWebHookedEvent notification, CancellationToken cancellationToken)
        {
            var payload = notification.WebHook;
            var @event = new EmailServiceWebHookedIntegrationEvent(payload, notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
