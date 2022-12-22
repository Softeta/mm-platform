using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record InviteSelectedCandidateViaEmailCommand(
        Guid JobId,
        IEnumerable<Guid> CandidateIds
    ): IRequest<JobCandidates>;
}
