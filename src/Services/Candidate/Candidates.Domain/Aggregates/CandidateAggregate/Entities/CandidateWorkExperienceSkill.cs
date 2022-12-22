using Domain.Seedwork.Shared.Entities;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateWorkExperienceSkill : SkillBase
    {
        public Guid CandidateWorkExperienceId { get; private set; }

        private CandidateWorkExperienceSkill() { }

        public CandidateWorkExperienceSkill(
            Guid skillId,
            Guid candidateWorkExperienceId,
            string code,
            Guid? aliasId,
            string? aliasCode)
        {
            Id = Guid.NewGuid();
            SkillId = skillId;
            CandidateWorkExperienceId = candidateWorkExperienceId;
            Code = code;
            AliasTo = Tag.Create(aliasId, aliasCode);
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
