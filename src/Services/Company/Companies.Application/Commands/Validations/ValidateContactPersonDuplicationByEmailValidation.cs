using MediatR;

namespace Companies.Application.Commands.Validations
{
    public record ValidateContactPersonDuplicationByEmailValidation(string Email) : INotification;
}
