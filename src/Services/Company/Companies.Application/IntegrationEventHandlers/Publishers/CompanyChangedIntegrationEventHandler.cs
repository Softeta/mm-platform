using Companies.Application.EventBus.Publishers;
using Companies.Application.IntegrationEventHandlers.Publishers.Payloads;
using Companies.Domain.Events;
using MediatR;

namespace Companies.Application.IntegrationEventHandlers.Publishers
{
    public class CompanyChangedIntegrationEventHandler : 
        INotificationHandler<CompanyUpdatedDomainEvent>,
        INotificationHandler<CompanyRejectedDomainEvent>,
        INotificationHandler<CompanyApprovedDomainEvent>
    {
        private readonly ICompanyEventBusPublisher _eventBus;

        public CompanyChangedIntegrationEventHandler(ICompanyEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(CompanyUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CompanyRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CompanyApprovedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(CompanyChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var payload = CompanyPayload.FromDomain(notification.Company);
            var @event = new CompanyChangedIntegrationEvent(payload, notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
