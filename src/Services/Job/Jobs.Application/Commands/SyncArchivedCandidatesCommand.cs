using MediatR;

namespace Jobs.Application.Commands
{
    public record SyncArchivedCandidatesCommand(ICollection<Guid> JobIds) : INotification;
}
