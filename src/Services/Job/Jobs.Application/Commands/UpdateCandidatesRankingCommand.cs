using Contracts.Job.JobCandidates;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record class UpdateCandidatesRankingCommand(
        Guid JobId,
        IEnumerable<CandidateRanking> CandidatesRanking
        ) : IRequest<JobCandidates>;
}
