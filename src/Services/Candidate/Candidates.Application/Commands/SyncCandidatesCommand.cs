using MediatR;

namespace Candidates.Application.Commands
{
    public record SyncCandidatesCommand(ICollection<Guid> CandidateIds) : INotification;
}
