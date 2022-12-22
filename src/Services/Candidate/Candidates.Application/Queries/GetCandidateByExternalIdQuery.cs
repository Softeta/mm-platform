using Candidates.Domain.Aggregates.CandidateAggregate;
using MediatR;

namespace Candidates.Application.Queries
{
    public record GetCandidateByExternalIdQuery(Guid ExternalId) : IRequest<Candidate?>;
}
