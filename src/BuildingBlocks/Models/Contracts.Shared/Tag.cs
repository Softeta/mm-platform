using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Contracts.Shared
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;

        public static Tag? From(Guid? id, string? code)
        {
            if (id is null || code is null) return null;

            return new Tag
            {
                Id = id.Value,
                Code = code,
            };
        }

        public static Tag? FromDomain(ValueObjects.Tag? alias)
        {
            if (alias is null) return null;

            return new Tag
            {
                Id = alias.Id,
                Code = alias.Code
            };
        }
    }
}
