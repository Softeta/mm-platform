using MediatR;
using Scheduler.Job.EventBus.Publishers;
using Scheduler.Job.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.Job.IntegrationEventHandlers
{
    public class ScheduledIntegrationEventHandler : 
        INotificationHandler<DeleteCandidatesScheduledEvent>,
        INotificationHandler<SyncRegistryCenterCompaniesScheduledEvent>
    {
        private readonly ISchedulerJobEventBusPublisher _eventBus;

        public ScheduledIntegrationEventHandler(ISchedulerJobEventBusPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(DeleteCandidatesScheduledEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(SyncRegistryCenterCompaniesScheduledEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(ScheduledEvent notification, CancellationToken cancellationToken)
        {
            var @event = new ScheduledIntegrationEvent(notification.EmittedAt);

            await _eventBus.PublishAsync(@event, notification.EventName, cancellationToken);
        }
    }
}
