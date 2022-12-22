using Jobs.Application.EventBus.Publishers;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads;
using Jobs.Domain.Events.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.IntegrationEventHandlers.Publishers
{
    public class JobCandidateShortlistActivatedViaEmailIntegrationEventHandler : INotificationHandler<JobCandidatesSharedShortlistViaEmailDomainEvent>
    {
        private readonly IJobCandidatesEventBusPublisher _eventBus;

        public JobCandidateShortlistActivatedViaEmailIntegrationEventHandler(IJobCandidatesEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(JobCandidatesSharedShortlistViaEmailDomainEvent notification, CancellationToken cancellationToken)
        {
            var payload = JobCandidatesPayload.FromDomain(notification.JobCandidates);
            var @event = new JobCandidateShortlistActivatedViaEmailIntegrationEvent(
                payload, 
                notification.ContactEmail,
                notification.ContactFirstName,
                notification.ContactSystemLanguage,
                notification.ContactExternalId,
                notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
