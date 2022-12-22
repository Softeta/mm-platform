using Domain.Seedwork.Shared.Entities;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateDesiredSkill : SkillBase
    {
        public Guid CandidateId { get; private set; }

        private CandidateDesiredSkill() { }

        public CandidateDesiredSkill(Guid skillId, Guid candidateId, string code, Guid? aliasId, string? aliasCode)
        {
            Id = Guid.NewGuid();
            SkillId = skillId;
            CandidateId = candidateId;
            Code = code;
            AliasTo = Tag.Create(aliasId, aliasCode);
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
