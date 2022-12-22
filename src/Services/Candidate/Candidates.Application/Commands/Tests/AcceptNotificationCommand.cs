using MediatR;

namespace Candidates.Application.Commands.Tests
{
    public record AcceptNotificationCommand(Guid CandidateId, Guid ExternalId, string Json) : INotification;
}
