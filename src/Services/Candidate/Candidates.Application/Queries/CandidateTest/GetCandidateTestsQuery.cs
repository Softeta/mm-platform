using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using MediatR;

namespace Candidates.Application.Queries.CandidateTest
{
    public record GetCandidateTestsQuery(
        Guid CandidateId,
        Guid UserId,
        string Scope) : IRequest<CandidateTests?>;
}
