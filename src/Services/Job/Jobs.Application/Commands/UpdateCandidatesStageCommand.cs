using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record UpdateCandidatesStageCommand(
        Guid JobId,
        SelectedCandidateStage Stage,
        IEnumerable<Guid> CandidateIds
    ) : IRequest<JobCandidates>;
}
