using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record UpdateCandidateBriefCommand(
        Guid JobId,
        Guid CandidateId, 
        string? Brief) : IRequest<JobCandidates>;
}
