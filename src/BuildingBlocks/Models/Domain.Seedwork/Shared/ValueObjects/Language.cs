namespace Domain.Seedwork.Shared.ValueObjects
{
    public class Language : ValueObject<Language>
    {
        public Guid Id { get; init; }
        public string Code { get; init; } = null!;
        public string Name { get; init; } = null!;

        public Language(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Id;
            yield return Code;
            yield return Name;
        }
    }
}
