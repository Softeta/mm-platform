using Candidates.Domain.Aggregates.CandidateAggregate;

namespace Candidates.Application.Commands
{
    public record UpdateCandidateOpenForOpportunitiesCommand(
        Guid CandidateId,
        bool OpenForOpportunities,
        Guid UserId,
        string Scope
        ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
