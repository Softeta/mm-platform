using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public record RequestEmailVerificationCommand(Guid ExternalId) : INotification;
}
