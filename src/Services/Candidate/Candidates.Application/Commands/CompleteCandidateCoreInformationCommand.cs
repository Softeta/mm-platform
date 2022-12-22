using Candidates.Domain.Aggregates.CandidateAggregate;

namespace Candidates.Application.Commands
{
    public record CompleteCandidateCoreInformationCommand(
        Guid CandidateId,
        Guid UserId,
        string Scope) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
