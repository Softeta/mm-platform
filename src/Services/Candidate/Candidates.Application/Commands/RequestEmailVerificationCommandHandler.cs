using Candidates.Application.Queries;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Candidates.Application.Commands
{
    public class RequestEmailVerificationCommandHandler : IRequestHandler<RequestEmailVerificationCommand, Candidate?>
    {
        private readonly ICandidateRepository _candidateRepository;

        public RequestEmailVerificationCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<Candidate?> Handle(RequestEmailVerificationCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByExternalidAsync(request.ExternalId);

            if (candidate is null) return null;

            candidate.RequestEmailVerification();

            _candidateRepository.Update(candidate);
            await _candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
