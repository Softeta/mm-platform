using BackOffice.Users.CacheRefresher.EventBus;
using EventBus.Constants;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher.EventHandlers
{
    internal class BackOfficeUsersUpdatedEventHandler : INotificationHandler<BackOfficeUsersUpdatedEvent>
    {
        private readonly IBackOfficeUserEventBusPublisher _eventBusPublisher;

        public BackOfficeUsersUpdatedEventHandler(IBackOfficeUserEventBusPublisher eventBusPublisher)
        {
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task Handle(BackOfficeUsersUpdatedEvent request, CancellationToken cancellationToken)
        {
            var events = request.Users
                .Select(x => new BackOfficeUserChangedIntegrationEvent(UserPayload.FromEntity(x)));

            await _eventBusPublisher.PublishAsync(
                events, 
                Topics.BackOfficeUserChanged.Filters.BackOfficeUserUpdated, 
                cancellationToken);
        }
    }
}
