using MediatR;

namespace Candidates.Application.Commands
{
    public record RejectCandidateCommand(
        Guid CandidateId) : INotification;
}
