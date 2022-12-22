using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record InviteSelectedCandidateViaLinkCommand(
        Guid JobId,
        IEnumerable<Guid> CandidateIds
    ) : IRequest<JobCandidates>;
}
