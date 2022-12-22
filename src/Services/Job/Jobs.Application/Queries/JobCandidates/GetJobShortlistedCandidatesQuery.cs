using Contracts.Job.SelectedCandidates.Responses;
using MediatR;

namespace Jobs.Application.Queries.JobsCandidates
{
    public record GetJobShortlistedCandidatesQuery(
        Guid JobId, 
        int PageNumber,
        int PageSize) : IRequest<JobSelectedCandidatesResponse>;
}
