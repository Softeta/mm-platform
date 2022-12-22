using Contracts.Candidate.CandidateJobs.Responses;
using Domain.Seedwork.Enums;
using MediatR;

namespace Candidates.Application.Queries
{
    public record GetCandidateSelectedInJobsQuery(
        Guid CandidateId,
        IEnumerable<SelectedCandidateStage>? SelectedCandidateStages,
        bool? IsInvited,
        int PageNumber,
        int PageSize
    ) : IRequest<GetCandidateSelectedInJobsResponse>;
}
