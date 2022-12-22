using Jobs.Application.EventBus.Publishers;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads;
using Jobs.Domain.Events.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.IntegrationEventHandlers.Publishers
{
    internal class JobCandidatesChangedIntegrationEventHandler :
        INotificationHandler<SelectedCandidatesAddedDomainEvent>,
        INotificationHandler<SelectedCandidatesUpdatedDomainEvent>,
        INotificationHandler<ArchivedCandidatesUpdatedDomainEvent>,
        INotificationHandler<JobCandidatesInformationUpdatedDomainEvent>,
        INotificationHandler<JobCandidatesJobStageUpdatedDomainEvent>,
        INotificationHandler<JobCandidatesHiredDomainEvent>
    {
        private readonly IJobCandidatesEventBusPublisher _eventBus;

        public JobCandidatesChangedIntegrationEventHandler(IJobCandidatesEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(SelectedCandidatesAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(SelectedCandidatesUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(ArchivedCandidatesUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobCandidatesInformationUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobCandidatesJobStageUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobCandidatesHiredDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(JobCandidatesChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var payload = JobCandidatesPayload.FromDomain(notification.JobCandidates);
            var @event = new JobCandidatesChangedIntegrationEvent(payload, notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
