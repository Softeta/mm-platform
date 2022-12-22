using EmailService.Send.Events;
using MediatR;

namespace EmailService.Send.Commands.JobReceiver
{
    internal record SendAskedForJobApprovalCommand(
        string FilterName,
        JobShareChangedEvent JobShareChangedEvent)
        : INotification;
}
