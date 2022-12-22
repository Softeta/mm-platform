using MediatR;

namespace Candidates.Application.Queries
{
    public record GetAllCandidateIdsQuery() : IRequest<List<Guid>>;
}
