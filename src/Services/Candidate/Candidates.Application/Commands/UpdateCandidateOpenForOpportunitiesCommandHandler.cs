using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;

namespace Candidates.Application.Commands
{
    public class UpdateCandidateOpenForOpportunitiesCommandHandler : ModifyCandidateBaseCommandHandler<UpdateCandidateOpenForOpportunitiesCommand, Candidate>
    {
        public UpdateCandidateOpenForOpportunitiesCommandHandler(ICandidateRepository candidateRepository) : base(candidateRepository)
        {
        }

        protected override async Task<Candidate> Handle(UpdateCandidateOpenForOpportunitiesCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            candidate.UpdateOpenForOpportunities(request.OpenForOpportunities);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
