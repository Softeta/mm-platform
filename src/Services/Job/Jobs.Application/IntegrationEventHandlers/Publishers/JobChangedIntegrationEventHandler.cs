using Jobs.Application.EventBus.Publishers;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads;
using Jobs.Domain.Events.JobAggregate;
using MediatR;

namespace Jobs.Application.IntegrationEventHandlers.Publishers
{
    internal class JobChangedIntegrationEventHandler :
        INotificationHandler<JobCreatedDomainEvent>,
        INotificationHandler<JobApprovedDomainEvent>,
        INotificationHandler<JobPublishedDomainEvent>,
        INotificationHandler<JobRejectedDomainEvent>,
        INotificationHandler<JobUpdatedDomainEvent>,
        INotificationHandler<JobSkillSyncedDomainEvent>,
        INotificationHandler<JobPositionSyncedDomainEvent>,
        INotificationHandler<JobInitializedDomainEvent>
    {
        private readonly IJobEventBusPublisher _eventBus;

        public JobChangedIntegrationEventHandler(IJobEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(JobCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobApprovedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobSkillSyncedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobPositionSyncedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobInitializedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(JobPublishedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(JobChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var payload = JobPayload.FromDomain(notification.Job);
            var @event = new JobChangedIntegrationEvent(payload, notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
