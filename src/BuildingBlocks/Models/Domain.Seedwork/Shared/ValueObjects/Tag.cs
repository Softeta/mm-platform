namespace Domain.Seedwork.Shared.ValueObjects
{
    public class Tag : ValueObject<Tag>
    {
        public Guid Id { get; init; }
        public string Code { get; init; } = null!;

        private Tag() { }

        private Tag(Guid id, string code)
        {
            Id = id;
            Code = code;
        }

        public static Tag? Create(Guid? id, string? code)
        {
            if (id is null || code is null) return null;
            return new Tag(id.Value, code);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Id;
            yield return Code;
        }
    }

}
