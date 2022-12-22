using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using MediatR;

namespace Jobs.Application.Queries.JobsCandidates
{
    public record GetJobShortlistedCandidateQuery(Guid JobId, Guid CandidateId) : IRequest<JobSelectedCandidate>;
}
