using MediatR;

namespace Jobs.Application.Commands
{
    public record ApproveJobCommand(Guid JobId) : INotification;
}
