using Candidates.Domain.Aggregates.CandidateAggregate;
using MediatR;

namespace Candidates.Application.Queries
{
    public record GetCandidateByIdQuery(Guid Id) : IRequest<Candidate?>;
}
