namespace Domain.Seedwork.Shared.Entities
{
    public abstract class HobbyBase : Entity
    {
        public Guid HobbyId { get; protected set; }
        public string Code { get; protected set; } = null!;
    }
}
