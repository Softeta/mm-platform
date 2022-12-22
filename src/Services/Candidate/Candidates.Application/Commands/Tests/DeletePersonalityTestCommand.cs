using MediatR;

namespace Candidates.Application.Commands.Tests
{
    public record DeletePersonalityTestCommand(Guid CandidateId, string PackageInstanceId) : INotification;
}
