using Domain.Seedwork.Shared.Entities;
using Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Domain.Aggregates.JobAggregate.Entities
{
    public class JobSkill : SkillBase
    {
        public Guid JobId { get; private set; }

        private JobSkill() { }

        public JobSkill(Guid skillId, Guid jobId, string code, Guid? aliasToId, string? aliasToCode)
        {
            Id = Guid.NewGuid();
            SkillId = skillId;
            JobId = jobId;
            Code = code;
            AliasTo = Tag.Create(aliasToId, aliasToCode);
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
