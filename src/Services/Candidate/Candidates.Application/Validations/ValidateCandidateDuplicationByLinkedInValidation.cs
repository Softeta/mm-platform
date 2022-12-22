using MediatR;

namespace Candidates.Application.Validations
{
    public record ValidateCandidateDuplicationByLinkedInValidation(string? LinkedInUrl) : INotification;
}
