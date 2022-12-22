using Contracts.Job.SelectedCandidates.Requests;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record AddSelectedCandidatesCommand(
        Guid JobId,
        IEnumerable<JobSelectedCandidateRequest> SelectedCandidates
    ) : IRequest<JobCandidates>;
}
