using MediatR;

namespace Jobs.Application.Queries.JobsCandidates
{
    public record GetJobCandidatesByCandidateIdQuery(Guid CandidateId) : IRequest<ICollection<Guid>>;
}
