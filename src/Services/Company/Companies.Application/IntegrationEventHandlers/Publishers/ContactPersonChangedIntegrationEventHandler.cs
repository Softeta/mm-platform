using Companies.Application.EventBus.Publishers;
using Companies.Application.IntegrationEventHandlers.Publishers.Payloads;
using Companies.Domain.Events;
using MediatR;

namespace Companies.Application.IntegrationEventHandlers.Publishers
{
    public class ContactPersonChangedIntegrationEventHandler : 
        INotificationHandler<ContactPersonRegisteredDomainEvent>,
        INotificationHandler<ContactPersonAddedDomainEvent>,
        INotificationHandler<ContactPersonUpdatedDomainEvent>,
        INotificationHandler<ContactPersonEmailVerificationRequestedDomainEvent>,
        INotificationHandler<ContactPersonRejectedDomainEvent>,
        INotificationHandler<ContactPersonLinkedDomainEvent>
    {
        private readonly IContactPersonEventBusPublisher _eventBus;

        public ContactPersonChangedIntegrationEventHandler(IContactPersonEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(ContactPersonRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(ContactPersonAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(ContactPersonUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(ContactPersonRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(ContactPersonEmailVerificationRequestedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(ContactPersonLinkedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(ContactPersonChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var payload = ContactPersonPayload.FromDomain(notification.Company, notification.ContactPerson);
            var @event = new ContactPersonChangedIntegrationEvent(payload, notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
