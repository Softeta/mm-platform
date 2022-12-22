using MediatR;

namespace Jobs.Application.Commands
{
    public record SyncSelectedCandidatesCommand(ICollection<Guid> JobIds) : INotification;
}
