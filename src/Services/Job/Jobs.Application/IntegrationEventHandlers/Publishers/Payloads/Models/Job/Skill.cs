using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class Skill
    {
        public Guid SkillId { get; set; }
        public string Code { get; set; } = null!;
        public Tag? AliasTo { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public static Skill FromDomain(JobSkill jobSkill)
        {
            return new Skill
            {
                SkillId = jobSkill.SkillId,
                Code = jobSkill.Code,
                AliasTo = Tag.FromDomain(jobSkill.AliasTo),
                CreatedAt = jobSkill.CreatedAt
            };
        }
    }
}
