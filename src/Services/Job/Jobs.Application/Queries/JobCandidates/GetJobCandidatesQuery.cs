using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Queries.JobsCandidates
{
    public record GetJobCandidatesQuery(Guid Id) : IRequest<JobCandidates>;
}
