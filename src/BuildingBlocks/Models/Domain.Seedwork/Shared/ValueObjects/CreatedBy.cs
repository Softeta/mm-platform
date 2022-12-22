using Domain.Seedwork.Enums;

namespace Domain.Seedwork.Shared.ValueObjects
{
    public class CreatedBy : ValueObject<CreatedBy>
    {
        public Guid Id { get; init; }
        public Scope Scope { get; init; }

        public CreatedBy(Guid id, Scope scope)
        {
            Scope = scope;
            Id = id;
        }
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Id;
            yield return Scope;
        }
    }
}
