using Candidates.Infrastructure.Persistence;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Application.Validations
{
    public class ValidateCandidateDuplicationByEmailValidationHandler : INotificationHandler<ValidateCandidateDuplicationByEmailValidation>
    {
        private readonly ICandidateContext _candidateContext;

        public ValidateCandidateDuplicationByEmailValidationHandler(ICandidateContext candidateContext)
        {
            _candidateContext = candidateContext;
        }

        public async Task Handle(ValidateCandidateDuplicationByEmailValidation notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(notification.Email)) return;

            var candidate = await _candidateContext.Candidates
                .AsNoTracking()
                .Where(x => x.Email != null && x.Email.Address == notification.Email)
                .Select(x => new { x.Id, x.FirstName, x.LastName })
                .SingleOrDefaultAsync(cancellationToken);

            if (candidate is not null)
            {
                throw new ConflictException($"Candidate with {notification.Email} email already exists",
                    ErrorCodes.Conflict.CandidateAlreadyExists,
                    new string[] { $"{candidate.FirstName} {candidate.LastName}", notification.Email, candidate.Id.ToString() });
            }
        }
    }
}
