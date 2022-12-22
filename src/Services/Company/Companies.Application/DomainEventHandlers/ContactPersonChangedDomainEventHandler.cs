using Companies.Domain.Events;
using Companies.Infrastructure.Clients;
using Domain.Seedwork.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Companies.Application.DomainEventHandlers
{
    internal class ContactPersonChangedDomainEventHandler :
        INotificationHandler<ContactPersonRegisteredDomainEvent>,
        INotificationHandler<ContactPersonLinkedDomainEvent>
    {
        private readonly IMsGraphServiceClient _msGraphServiceClient;
        private readonly ILogger<ContactPersonChangedDomainEventHandler> _logger;

        public ContactPersonChangedDomainEventHandler(
            IMsGraphServiceClient msGraphServiceClient,
            ILogger<ContactPersonChangedDomainEventHandler> logger)
        {
            _msGraphServiceClient = msGraphServiceClient;
            _logger = logger;
        }

        public async Task Handle(ContactPersonRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        public async Task Handle(ContactPersonLinkedDomainEvent notification, CancellationToken cancellationToken)
        {
            await HandleAsync(notification, cancellationToken);
        }

        private async Task HandleAsync(ContactPersonChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var isContactPersonAdmin = notification.ContactPerson.Role == ContactPersonRole.Admin;

            if (notification.ContactPerson.ExternalId is null)
            {
                _logger.LogCritical("No external ID found {ContactPerson}", notification.ContactPerson);
                return;
            }

            await _msGraphServiceClient.UpdateUserAttributesAsync(
                notification.ContactPerson.ExternalId.Value,
                notification.ContactPerson.CompanyId,
                notification.ContactPerson.Id,
                isContactPersonAdmin,
                cancellationToken);
        }
    }
}
