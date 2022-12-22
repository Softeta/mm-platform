using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Candidates.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Options;

namespace Candidates.Application.Commands
{
    public class VerifyCandidateCommandHandler : IRequestHandler<VerifyCandidateCommand, Candidate>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly VerificationSettings _verificationSettings;

        public VerifyCandidateCommandHandler(ICandidateRepository candidateRepository, IOptions<VerificationSettings> options)
        {
            _candidateRepository = candidateRepository;
            _verificationSettings = options.Value;
        }

        public async Task<Candidate> Handle(VerifyCandidateCommand notification, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetAsync(notification.CandidateId);
            
            candidate.VerifyEmail(notification.Key, _verificationSettings.ExpiresInMinutes);

            _candidateRepository.Update(candidate);
            await _candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
