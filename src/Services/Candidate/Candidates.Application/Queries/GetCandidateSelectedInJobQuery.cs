using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using MediatR;

namespace Candidates.Application.Queries
{
    public record GetCandidateSelectedInJobQuery(
        Guid JobId,
        Guid CandidateId) : IRequest<CandidateSelectedInJob?>;
}
