using Candidates.Application.NotificationHandlers.Events;
using MediatR;

namespace Candidates.Application.Commands.Tests
{
    public record SaveLogicalTestRaportsCommand(
        Guid CandidateId, 
        Guid ExternalId, 
        PackageInstanceScored Payload) : INotification;
}
