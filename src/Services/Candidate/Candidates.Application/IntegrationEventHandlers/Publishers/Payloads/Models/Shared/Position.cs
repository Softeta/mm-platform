using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared
{
    internal class Position
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public Tag? AliasTo { get; set; }

        public static Position FromDomain(ValueObjects.Position position)
        {
            return new Position
            {
                Id = position.Id,
                Code = position.Code,
                AliasTo = Tag.FromDomain(position.AliasTo)
            };
        }

        public static Position? FromDomainNullable(ValueObjects.Position? position)
        {
            if (position is null) return null;

            return new Position
            {
                Id = position.Id,
                Code = position.Code,
                AliasTo = Tag.FromDomain(position.AliasTo)
            };
        }
    }
}
