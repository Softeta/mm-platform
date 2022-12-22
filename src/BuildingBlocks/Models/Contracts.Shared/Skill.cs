using Domain.Seedwork.Shared.Entities;

namespace Contracts.Shared
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public Tag? AliasTo { get; set; }

        public static Skill FromDomain(SkillBase candidateSkill)
        {
            return new Skill
            {
                Id = candidateSkill.SkillId,
                Code = candidateSkill.Code,
                AliasTo = Tag.FromDomain(candidateSkill.AliasTo)
            };
        }

        public static Skill? From(Guid? id, string? code)
        {
            if (!id.HasValue || string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            return new Skill
            {
                Id = id.Value,
                Code = code
            };
        }
    }
}
