using Candidates.Application.NotificationHandlers.Events;
using MediatR;

namespace Candidates.Application.Commands.Tests
{
    public record SavePersonalityTestRaportsCommand(
        Guid CandidateId, 
        Guid ExternalId, 
        PackageInstanceScored Payload) : INotification;
}
