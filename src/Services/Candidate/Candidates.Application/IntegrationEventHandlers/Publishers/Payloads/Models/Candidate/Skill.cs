using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Domain.Seedwork.Shared.Entities;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    internal class Skill
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public Tag? AliasTo { get; set; }

        public static Skill FromDomain(SkillBase skill)
        {
            return new Skill
            {
                Id = skill.Id,
                Code = skill.Code,
                AliasTo = Tag.FromDomain(skill.AliasTo)
            };
        }
    }
}
