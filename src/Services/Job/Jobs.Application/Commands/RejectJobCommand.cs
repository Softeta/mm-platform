using MediatR;

namespace Jobs.Application.Commands
{
    public record RejectJobCommand(Guid JobId) : INotification;
}
