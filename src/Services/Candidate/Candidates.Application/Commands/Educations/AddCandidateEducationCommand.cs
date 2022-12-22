using Candidates.Domain.Aggregates.CandidateAggregate;
using Contracts.Shared.Requests;

namespace Candidates.Application.Commands.Educations
{
    public record AddCandidateEducationCommand(
        Guid CandidateId,
        string SchoolName,
        string Degree,
        string FieldOfStudy,
        DateTimeOffset From,
        DateTimeOffset? To,
        string? StudiesDescription,
        bool IsStillStudying,
        AddFileCacheRequest? Certificate,
        Guid UserId,
        string Scope
    ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
