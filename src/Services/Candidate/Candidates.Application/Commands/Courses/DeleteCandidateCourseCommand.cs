using Candidates.Domain.Aggregates.CandidateAggregate;

namespace Candidates.Application.Commands.Courses
{
    public record DeleteCandidateCourseCommand(
        Guid CandidateId,
        Guid CourseId,
        Guid UserId,
        string Scope)
        : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
