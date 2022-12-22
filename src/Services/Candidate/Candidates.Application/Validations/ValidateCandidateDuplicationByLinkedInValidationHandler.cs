using Candidates.Infrastructure.Persistence;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Application.Validations
{
    public class ValidateCandidateDuplicationByLinkedInValidationHandler : INotificationHandler<ValidateCandidateDuplicationByLinkedInValidation>
    {
        private readonly ICandidateContext _candidateContext;

        public ValidateCandidateDuplicationByLinkedInValidationHandler(ICandidateContext candidateContext)
        {
            _candidateContext = candidateContext;
        }

        public async Task Handle(ValidateCandidateDuplicationByLinkedInValidation notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(notification.LinkedInUrl)) return;

            var candidate = await _candidateContext.Candidates
                .AsNoTracking()
                .Where(x => x.LinkedInUrl == notification.LinkedInUrl)
                .Select(x => new { x.Id, x.FirstName, x.LastName })
                .SingleOrDefaultAsync(cancellationToken);

            if (candidate is not null)
            {
                throw new ConflictException($"Candidate with {notification.LinkedInUrl} linkedIn already exists",
                    ErrorCodes.Conflict.CandidateLinkedInAlreadyExists,
                    new string[] { $"{candidate.FirstName} {candidate.LastName}", 
                        notification.LinkedInUrl,
                        candidate.Id.ToString() });
            }
        }
    }
}
