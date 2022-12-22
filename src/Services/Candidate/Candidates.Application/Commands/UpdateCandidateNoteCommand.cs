using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;

namespace Candidates.Application.Commands
{
    public record UpdateCandidateNoteCommand(
        Guid CandidateId,
        Guid UserId,
        string Scope,
        string? Note,
        DateTimeOffset? EndDate) : ModifyCandidateBaseCommand<Note?>(CandidateId, UserId, Scope);
}
