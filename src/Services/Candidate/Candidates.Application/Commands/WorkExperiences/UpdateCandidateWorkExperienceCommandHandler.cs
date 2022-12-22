using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Infrastructure.Persistence.Repositories;

namespace Candidates.Application.Commands.WorkExperiences
{
    public class UpdateCandidateWorkExperienceCommandHandler : ModifyCandidateBaseCommandHandler<UpdateCandidateWorkExperienceCommand, Candidate>
    {
        public UpdateCandidateWorkExperienceCommandHandler(ICandidateRepository candidateRepository)
            : base(candidateRepository)
        {
        }

        protected override async Task<Candidate> Handle(UpdateCandidateWorkExperienceCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            candidate.UpdateWorkExperience(
                request.WorkExperienceId,
                request.Type,
                request.CompanyName,
                request.Position.Id,
                request.Position.Code,
                request.Position.AliasTo?.Id,
                request.Position.AliasTo?.Code,
                request.From,
                request.To,
                request.JobDescription,
                request.IsCurrentJob,
                request.Skills.Select(x => new CandidateWorkExperienceSkill(x.Id, candidate.Id, x.Code, x.AliasTo?.Id, x.AliasTo?.Code)));

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
        }
    }
}
