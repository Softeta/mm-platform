using Candidates.Application.Commands.Tests;
using Candidates.Application.NotificationHandlers.Events;
using Candidates.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Candidates.Application.NotificationHandlers
{
    public class PackageInstanceScoredHandler : INotificationHandler
    {
        public const string PackageInstanceScored = "PackageInstanceScored";
        private readonly TalogySettings _talogySettings;
        private readonly IMediator _mediator;

        public PackageInstanceScoredHandler(
            IMediator mediator,
            IOptions<TalogySettings> talogySettings)
        {
            _mediator = mediator;
            _talogySettings = talogySettings.Value;
        }

        public string NotificationType => PackageInstanceScored;

        public async Task ExecuteAsync(Guid candidateId, Guid externalId, string notification)
        {
            var payload = JsonConvert.DeserializeObject<PackageInstanceScored>(notification);

            if (payload is null)
            {
                throw new ArgumentException("Failed to deserialize notification {Notification}", notification);
            }

            if (_talogySettings.PackageTypes.Logic == payload.PackageTypeId)
            {
                await _mediator.Publish(new SaveLogicalTestRaportsCommand(candidateId, externalId, payload));
                return;
            }

            if (_talogySettings.PackageTypes.Personality == payload.PackageTypeId)
            {
                await _mediator.Publish(new SavePersonalityTestRaportsCommand(candidateId, externalId, payload));
                return;
            }
        }
    }
}
