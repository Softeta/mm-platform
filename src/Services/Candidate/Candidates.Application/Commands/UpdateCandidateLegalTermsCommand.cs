using Candidates.Domain.Aggregates.CandidateAggregate;

namespace Candidates.Application.Commands
{
    public record UpdateCandidateLegalTermsCommand(
        Guid CandidateId,
        Guid UserId,
        string Scope,
        bool TermsAgreement,
        bool MarketingAgreement
    ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
