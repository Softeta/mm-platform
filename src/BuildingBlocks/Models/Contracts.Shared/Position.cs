using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Contracts.Shared
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public Tag? AliasTo { get; set; }

        public static Position? From(Guid? id, string? code, Guid? aliasId, string? aliasCode)
        {
            if (!id.HasValue || string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            return new Position
            {
                Id = id.Value,
                Code = code,
                AliasTo = Tag.From(aliasId, aliasCode)
            };
        }

        public static Position FromDomainNotNullable(ValueObjects.Position position)
        {
            return new Position
            {
                Id = position.Id,
                Code = position.Code,
                AliasTo = Tag.FromDomain(position.AliasTo)
            };
        }

        public static Position? FromDomain(ValueObjects.Position? position)
        {
            if (position is null)
            {
                return null;
            }

            return FromDomainNotNullable(position);
        }
    }
}
