using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record ArchiveCandidatesCommand(
        Guid JobId,
        ArchivedCandidateStage Stage,
        IEnumerable<Guid> CandidateIds
    ) : IRequest<JobCandidates>;
}
