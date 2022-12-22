using Candidates.Application.EventBus.Publishers;
using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads;
using Candidates.Domain.Events.CandidateJobsAggregate;
using MediatR;

namespace Candidates.Application.IntegrationEventHandlers.Publishers
{
    internal class CandidateJobChangedIntegrationEventHandler :
        INotificationHandler<CandidateJobRejectedDomainEvent>
    {
        private readonly ICandidateJobsEventBusPublisher _eventBus;

        public CandidateJobChangedIntegrationEventHandler(ICandidateJobsEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task Handle(CandidateJobRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task HandleAsync(CandidateJobChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var payload = CandidateJobChangedPayload.FromDomain(notification.CandidateJobs, notification.JobId);
            var @event = new CandidateJobChangedIntegrationEvent(payload, notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
