using MediatR;

namespace Candidates.Application.Validations
{
    public record ValidateIsAllowedReadCandidateValidation(Guid? CompanyId, Guid CandidateId) : INotification;
}
