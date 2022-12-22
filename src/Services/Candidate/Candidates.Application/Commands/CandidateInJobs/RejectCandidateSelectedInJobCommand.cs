using MediatR;

namespace Candidates.Application.Commands.CandidateInJobs
{
    public record RejectCandidateSelectedInJobCommand(Guid CandidateId, Guid JobId) : INotification;
}
