using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using MediatR;

namespace Jobs.Application.Queries.JobCandidatesFilters;

public record GetJobCandidatesFiltersQuery(Guid JobId, Guid UserId) : IRequest<IEnumerable<JobCandidatesFilter>>;