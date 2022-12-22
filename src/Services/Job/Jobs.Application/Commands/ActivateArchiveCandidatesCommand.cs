using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record ActivateArchiveCandidatesCommand(
        Guid JobId,
        IEnumerable<Guid> CandidateIds
    ) : IRequest<JobCandidates>;
}
