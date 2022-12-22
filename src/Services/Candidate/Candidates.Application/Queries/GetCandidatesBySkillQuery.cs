using MediatR;

namespace Candidates.Application.Queries
{
    public record GetCandidatesBySkillQuery(Guid Id) : IRequest<List<Guid>>;
}
