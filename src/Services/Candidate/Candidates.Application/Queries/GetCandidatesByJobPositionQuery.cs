using MediatR;

namespace Candidates.Application.Queries
{
    public record GetCandidatesByJobPositionQuery(Guid Id) : IRequest<List<Guid>>;
}
