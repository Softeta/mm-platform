using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Contracts.Shared.Requests;
using MediatR;

namespace Candidates.Application.Commands.CandidateInJobs
{
    public record UpdateCandidateSelectedInJobCommand(
        Guid CandidateId,
        Guid JobId,
        UpdateFileCacheRequest? MotivationVideo, 
        string? CoverLetter) : IRequest<CandidateSelectedInJob>;
}
