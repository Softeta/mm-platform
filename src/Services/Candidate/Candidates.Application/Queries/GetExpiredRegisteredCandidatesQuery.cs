using Candidates.Application.Queries.Models;
using MediatR;

namespace Candidates.Application.Queries
{
    public record GetExpiredRegisteredCandidatesQuery() : IRequest<List<ExpiredRegisteredCandidate>>;
}
