using MediatR;

namespace Jobs.Application.Commands
{
    public record ShareJobViaEmailCommand(
        Guid JobId,
        string ReceiverEmail
        ) : IRequest<DateTimeOffset>;
}
