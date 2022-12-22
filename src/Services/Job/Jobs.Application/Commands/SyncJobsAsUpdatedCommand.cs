using MediatR;

namespace Jobs.Application.Commands
{
    public record SyncJobsAsUpdatedCommand(ICollection<Guid> JobIds) : INotification;
}
