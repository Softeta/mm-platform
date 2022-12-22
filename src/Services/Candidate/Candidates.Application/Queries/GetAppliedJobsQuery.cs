using Contracts.Candidate.CandidateJobs.Responses;
using Domain.Seedwork.Enums;
using MediatR;

namespace Candidates.Application.Queries
{
    public record GetAppliedJobsQuery(
        Guid CandidateId,
        CandidateAppliedToJobOrderBy? OrderBy,
        int PageNumber,
        int PageSize) : IRequest<GetCandidateAppliedToJobsResponse>;
}
