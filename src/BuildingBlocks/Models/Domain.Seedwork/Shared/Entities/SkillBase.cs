using Domain.Seedwork.Shared.ValueObjects;

namespace Domain.Seedwork.Shared.Entities
{
    public abstract class SkillBase : Entity
    {
        public Guid SkillId { get; protected set; }
        public string Code { get; protected set; } = null!;
        public Tag? AliasTo { get; protected set; }

        public void Sync(Guid? aliasId, string? aliasCode)
        {
            AliasTo = Tag.Create(aliasId, aliasCode);
        }
    }
}
