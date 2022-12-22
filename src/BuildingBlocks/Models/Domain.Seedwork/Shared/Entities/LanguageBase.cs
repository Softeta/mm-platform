using Domain.Seedwork.Shared.ValueObjects;

namespace Domain.Seedwork.Shared.Entities
{
    public abstract class LanguageBase : Entity
    {
        public Language Language { get; protected set; } = null!;
    }
}
