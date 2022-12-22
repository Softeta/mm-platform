using MediatR;

namespace Candidates.Application.Commands
{
    public record ApproveCandidateCommand(
        Guid CandidateId) : INotification;
}
