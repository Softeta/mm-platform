using Jobs.Application.EventBus.Publishers;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads;
using Jobs.Domain.Events.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.IntegrationEventHandlers.Publishers
{
    public class JobCandidateChangedIntegrationEventHandler : INotificationHandler<JobSelectedCandidateInvitedDomainEvent>
    {
        private readonly IJobCandidatesEventBusPublisher _eventBus;

        public JobCandidateChangedIntegrationEventHandler(IJobCandidatesEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(JobSelectedCandidateInvitedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(JobCandidateChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var payload = JobCandidatePayload.FromDomain(notification.JobCandidates, notification.JobCandidate);
            var @event = new JobCandidateChangedIntegrationEvent(payload, notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
