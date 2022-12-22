using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace Contracts.Candidate.WorkExperiences.Requests
{
    public class CandidateWorkExperienceRequest
    {
        public WorkExperienceType Type { get; set; }

        public string CompanyName { get; set; } = null!;

        public Position Position { get; set; } = null!;

        public DateTimeOffset From { get; set; }

        public DateTimeOffset? To { get; set; }

        public string? JobDescription { get; set; }

        public bool IsCurrentJob { get; set; }

        public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();
    }
}
