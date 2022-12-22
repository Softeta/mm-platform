using Candidates.Application.EventBus.Publishers;
using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads;
using Candidates.Domain.Events.CandidateAggregate;
using MediatR;

namespace Candidates.Application.IntegrationEventHandlers.Publishers
{
    public class CandidateChangedIntegrationEventHandler : 
        INotificationHandler<CandidateCreatedDomainEvent>,
        INotificationHandler<CandidateRegisteredDomainEvent>,
        INotificationHandler<CandidateUpdatedDomainEvent>,
        INotificationHandler<CandidateEmailVerificationRequestedDomainEvent>,
        INotificationHandler<CandidateApprovedDomainEvent>,
        INotificationHandler<CandidateRejectedDomainEvent>,
        INotificationHandler<CandidateSkillsSyncedDomainEvent>,
        INotificationHandler<CandidateJobPositionSyncedDomainEvent>
    {
        private readonly ICandidateEventBusPublisher _eventBus;

        public CandidateChangedIntegrationEventHandler(ICandidateEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(CandidateCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateApprovedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateEmailVerificationRequestedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateSkillsSyncedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(CandidateJobPositionSyncedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(CandidateChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var payload = CandidatePayload.FromDomain(notification.Candidate);
            var @event = new CandidateChangedIntegrationEvent(payload, notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
