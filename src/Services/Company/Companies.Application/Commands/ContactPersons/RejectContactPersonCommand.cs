using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public record RejectContactPersonCommand(
        Guid CompanyId,
        Guid ContactId) : INotification;
}
