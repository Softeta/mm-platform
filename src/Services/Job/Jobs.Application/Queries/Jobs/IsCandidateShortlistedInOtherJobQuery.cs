using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record IsCandidateShortlistedInOtherJobQuery(Guid JobId, Guid CandidateId) 
        : IRequest<bool>;
}
