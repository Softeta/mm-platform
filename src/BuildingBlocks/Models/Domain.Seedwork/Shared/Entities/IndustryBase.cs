namespace Domain.Seedwork.Shared.Entities
{
    public abstract class IndustryBase : Entity
    {
        public Guid IndustryId { get; protected set; }
        public string Code { get; protected set; } = null!;
    }
}
