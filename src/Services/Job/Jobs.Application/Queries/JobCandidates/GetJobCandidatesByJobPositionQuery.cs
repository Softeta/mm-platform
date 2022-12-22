using MediatR;

namespace Jobs.Application.Queries.JobsCandidates
{
    public record GetJobCandidatesByJobPositionQuery(Guid Id) : IRequest<List<Guid>>;
}
