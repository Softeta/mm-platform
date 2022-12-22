using Candidates.Application.EventBus.Publishers;
using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads;
using Candidates.Domain.Events.CandidateJobsAggregate;
using MediatR;

namespace Candidates.Application.IntegrationEventHandlers.Publishers
{
    public class CandidateJobsChangedIntegrationEventHandler :
        INotificationHandler<CandidateJobsAddedDomainEvent>,
        INotificationHandler<CandidateJobsUpdatedDomainEvent>,
        INotificationHandler<CandidateJobsArchivedDomainEvent>,
        INotificationHandler<CandidateJobsShortlistedDomainEvent>,
        INotificationHandler<CandidateJobsUnshortlistedDomainEvent>,
        INotificationHandler<CandidateJobsHiredDomainEvent>
    {
        private readonly ICandidateJobsEventBusPublisher _eventBus;

        public CandidateJobsChangedIntegrationEventHandler(ICandidateJobsEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(CandidateJobsAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateJobsUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateJobsArchivedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateJobsShortlistedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateJobsUnshortlistedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateJobsHiredDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(CandidateJobsChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var payload = CandidateJobsPayload.FromDomain(notification.CandidateJobs);
            var @event = new CandidateJobsChangedIntegrationEvent(payload, notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
