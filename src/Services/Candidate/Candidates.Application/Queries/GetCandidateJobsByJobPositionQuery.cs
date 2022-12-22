using MediatR;

namespace Candidates.Application.Queries
{
    public record GetCandidateJobsByJobPositionQuery(Guid Id) : IRequest<List<Guid>>;
}
