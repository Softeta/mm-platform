using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Contracts.Shared;
using Common = Contracts.Candidate.WorkExperiences.Responses;

namespace Candidates.Application.Contracts.WorkExperiences.Responses
{
    public class CandidateWorkExperienceResponse : Common.CandidateWorkExperienceResponse
    {
        public static Common.CandidateWorkExperienceResponse FromDomain(CandidateWorkExperience candidateWorkExperience)
        {
            return new Common.CandidateWorkExperienceResponse
            {
                Id = candidateWorkExperience.Id,
                Type = candidateWorkExperience.Type,
                CompanyName = candidateWorkExperience.CompanyName,
                Position = Position.FromDomainNotNullable(candidateWorkExperience.Position),
                From = candidateWorkExperience.Period.From,
                To = candidateWorkExperience.Period.To,
                JobDescription = candidateWorkExperience.JobDescription,
                IsCurrentJob = candidateWorkExperience.IsCurrentJob,
                Skills = candidateWorkExperience.Skills.Select(Skill.FromDomain)
            };
        }
    }
}
