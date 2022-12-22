using Candidates.Domain.Aggregates.CandidateAggregate.Entities;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate;

internal class WorkExperience
{
    public string CompanyName { get; set; } = null!;
    public bool IsCurrentJob { get; set; }
    public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();

    public static WorkExperience FromDomain(CandidateWorkExperience candidateWorkExperience)
    {
        return new WorkExperience
        {
            CompanyName = candidateWorkExperience.CompanyName,
            IsCurrentJob = candidateWorkExperience.IsCurrentJob,
            Skills = candidateWorkExperience.Skills.Select(Skill.FromDomain)
        };
    }
} 