using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;

namespace Candidates.Application.Commands
{
    public class UpdateCandidateSettingsCommandHandler : ModifyCandidateBaseCommandHandler<UpdateCandidateSettingsCommand, Candidate>
    {
        public UpdateCandidateSettingsCommandHandler(ICandidateRepository candidateRepository) : base(candidateRepository)
        {
        }

        protected override async Task<Candidate> Handle(UpdateCandidateSettingsCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            candidate.UpdateSettings(request.SystemLanguage, request.MarketingAcknowledgement);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
