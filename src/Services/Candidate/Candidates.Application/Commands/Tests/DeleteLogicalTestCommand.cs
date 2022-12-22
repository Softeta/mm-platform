using MediatR;

namespace Candidates.Application.Commands.Tests
{
    public record DeleteLogicalTestCommand(Guid CandidateId, string PackageInstanceId) : INotification;
}
