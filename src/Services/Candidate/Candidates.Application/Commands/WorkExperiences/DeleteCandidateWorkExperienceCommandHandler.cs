using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;

namespace Candidates.Application.Commands.WorkExperiences
{
    public class DeleteCandidateWorkExperienceCommandHandler : ModifyCandidateBaseCommandHandler<DeleteCandidateWorkExperienceCommand, Candidate>
    {
        public DeleteCandidateWorkExperienceCommandHandler(ICandidateRepository candidateRepository)
            : base(candidateRepository)
        {
        }

        protected override async Task<Candidate> Handle(DeleteCandidateWorkExperienceCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            candidate.DeleteWorkExperience(request.WorkExperienceId);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
