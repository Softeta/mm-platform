using Candidates.Application.NotificationHandlers;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Candidates.Application.Commands.Tests
{
    public class AcceptNotificationCommandHandler : INotificationHandler<AcceptNotificationCommand>
    {
        private readonly INotificationHandlersManager _notificationHandlersManager;
        private readonly ILogger<AcceptNotificationCommandHandler> _logger;

        public AcceptNotificationCommandHandler(
            INotificationHandlersManager notificationHandlersManager,
            ILogger<AcceptNotificationCommandHandler> logger)
        {
            _notificationHandlersManager = notificationHandlersManager;
            _logger = logger;
        }

        public async Task Handle(AcceptNotificationCommand request, CancellationToken cancellationToken)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(request.Json);

            string? notificationType = data?.notification?.ToString();

            if (string.IsNullOrWhiteSpace(notificationType))
            {
                _logger.LogError("Notification not recognized. {Json}", request.Json);
                return;
            }

            try
            {
                var handler = _notificationHandlersManager.GetHandler(notificationType);
                await handler.ExecuteAsync(request.CandidateId, request.ExternalId, request.Json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process notification {Notification}", request.Json);
            }
        }
    }
}
