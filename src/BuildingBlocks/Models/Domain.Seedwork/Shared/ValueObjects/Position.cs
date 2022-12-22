namespace Domain.Seedwork.Shared.ValueObjects
{
    public class Position : ValueObject<Position>
    {
        public Guid Id { get; init; }
        public string Code { get; init; } = null!;
        public Tag? AliasTo { get; init; }

        private Position() { }

        public Position(Guid id, string code, Guid? aliasId, string? aliasCode)
        {
            Id = id;
            Code = code;
            AliasTo = Tag.Create(aliasId, aliasCode);
        }

        public static Position? Create(Guid? id, string? code, Guid? aliasId, string? aliasCode)
        {
            if (!id.HasValue || string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            return new Position(id.Value, code, aliasId, aliasCode);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Id;
            yield return Code;
        }
    }
}
