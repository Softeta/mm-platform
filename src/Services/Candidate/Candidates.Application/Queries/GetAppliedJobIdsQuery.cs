using MediatR;

namespace Candidates.Application.Queries
{
    public record GetAppliedJobIdsQuery(
        Guid CandidateId) : IRequest<List<Guid>>;
}
