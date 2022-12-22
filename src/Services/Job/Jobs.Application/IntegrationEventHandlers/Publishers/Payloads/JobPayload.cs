using Domain.Seedwork.Enums;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads
{
    public class JobPayload
    {
        public Guid JobId { get; set; }
        public Company Company { get; set; } = null!;
        public Employee? Owner { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public YearExperience? YearExperience { get; set; }
        public DateTimeOffset? DeadlineDate { get; set; }
        public string? Description { get; set; }
        public JobStage Stage { get; set; }
        public bool IsPublished { get; set; }
        public Terms? Terms { get; set; }
        public DateTimeOffset? SharingDate { get; set; }
        public string? Location { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public IEnumerable<AssignedEmployee> AssignedEmployees { get; set; } = new List<AssignedEmployee>();
        public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();
        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();
        public IEnumerable<Seniority> Seniorities { get; set; } = new List<Seniority>();
        public IEnumerable<Language> Languages { get; set; } = new List<Language>();

        public static JobPayload FromDomain(Job job)
        {
            return new JobPayload
            {
                JobId = job.Id,
                Company = Company.FromDomain(job.Company),
                Owner = Employee.FromDomain(job.Owner),
                Position = Position.FromDomain(job.Position),
                YearExperience = YearExperience.FromDomain(job.YearExperience),
                DeadlineDate = job.DeadlineDate,
                Description = job.Description,
                Stage = job.Stage,
                IsPublished = job.IsPublished,
                SharingDate = job.Sharing?.Date,
                AssignedEmployees = job.AssignedEmployees.Select(AssignedEmployee.FromDomain),
                Skills = job.Skills.Select(Skill.FromDomain),
                Industries = job.Industries.Select(x => new Industry(x.IndustryId, x.Code, x.CreatedAt)),
                Seniorities = job.SeniorityLevels.Select(x => new Seniority(x.Seniority, x.CreatedAt)),
                Languages = job.Languages.Select(x => new Language(x.Language.Id, x.Language.Code, x.Language.Name, x.CreatedAt)),
                Terms = job.Terms is null ? null : Terms.FromDomain(job.Terms),
                Location = job.Location,
                CreatedAt = job.CreatedAt
            };
        }
    }
}
