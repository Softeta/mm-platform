using Candidates.Domain.Aggregates.CandidateAggregate;
using Domain.Seedwork.Enums;

namespace Candidates.Application.Commands
{
    public record UpdateCandidateSettingsCommand(
        Guid CandidateId,
        Guid UserId,
        string Scope,
        SystemLanguage SystemLanguage,
        bool MarketingAcknowledgement
    ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
