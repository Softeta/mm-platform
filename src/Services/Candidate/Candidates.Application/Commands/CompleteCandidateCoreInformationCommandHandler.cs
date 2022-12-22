using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;

namespace Candidates.Application.Commands
{
    public class CompleteCandidateCoreInformationCommandHandler : ModifyCandidateBaseCommandHandler<CompleteCandidateCoreInformationCommand, Candidate>
    {
        public CompleteCandidateCoreInformationCommandHandler(ICandidateRepository candidateRepository) : base(candidateRepository)
        {
        }

        protected override async Task<Candidate> Handle(
            CompleteCandidateCoreInformationCommand request,
            Candidate candidate, 
            CancellationToken cancellationToken)
        {
            candidate.CompleteCoreInformation();

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
