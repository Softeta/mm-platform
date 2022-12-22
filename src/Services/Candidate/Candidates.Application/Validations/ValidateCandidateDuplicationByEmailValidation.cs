using MediatR;

namespace Candidates.Application.Validations
{
    public record ValidateCandidateDuplicationByEmailValidation(string? Email) : INotification;
}
