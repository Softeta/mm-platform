using Domain.Seedwork.Enums;
using MediatR;

namespace Jobs.Application.Commands
{
    public record PublishJobCommand(
        Guid JobId
    ) : INotification;
}
