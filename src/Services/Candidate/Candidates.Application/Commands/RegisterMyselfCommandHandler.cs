using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Candidates.Application.Commands
{
    public class RegisterMyselfCommandHandler : IRequestHandler<RegisterMyselfCommand, Candidate>
    {
        private readonly ICandidateRepository _candidateRepository;

        public RegisterMyselfCommandHandler(ICandidateRepository candidateRepository)

        {
            _candidateRepository = candidateRepository;
        }

        public async Task<Candidate> Handle(RegisterMyselfCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByEmailAsync(request.Email);

            if (candidate is null)
            {
                candidate = new Candidate();
                candidate.RegisterMyself(
                    request.Email,
                    request.ExternalId,
                    request.SystemLanguage,
                    request.AcceptTermsAndConditions,
                    request.AcceptMarketingAcknowledgement);

                _candidateRepository.Add(candidate);
            }
            else
            {
                candidate.LinkCandidate(
                    request.ExternalId,
                    request.SystemLanguage,
                    request.AcceptTermsAndConditions,
                    request.AcceptMarketingAcknowledgement);

                _candidateRepository.Update(candidate);
            }
            
            await _candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
