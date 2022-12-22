using Candidates.Infrastructure.Persistence;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Application.Validations
{
    public class ValidateIsAllowedReadCandidateValidationHandler : INotificationHandler<ValidateIsAllowedReadCandidateValidation>
    {
        private readonly ICandidateContext _candidateContext;

        public ValidateIsAllowedReadCandidateValidationHandler(ICandidateContext candidateContext)
        {
            _candidateContext = candidateContext;
        }

        public async Task Handle(ValidateIsAllowedReadCandidateValidation notification, CancellationToken cancellationToken)
        {
            var selectedInJob = await _candidateContext.CandidateSelectedInJobs
                .Where(x => x.Company.Id == notification.CompanyId)
                .Where(x => x.CandidateId == notification.CandidateId)
                .Select(x => new { Stage = x.Stage }).FirstOrDefaultAsync();

            if (selectedInJob is null || !selectedInJob.Stage.IsShortlisted())
            {
                throw new ForbiddenException("Access denied", ErrorCodes.Forbidden.CandidateServiceAccessDenied);
            }
        }
    }
}
