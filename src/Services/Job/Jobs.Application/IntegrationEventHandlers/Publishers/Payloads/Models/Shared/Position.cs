using DomainValueObject = Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public Tag? AliasTo { get; set; }

        public static Position FromDomain(DomainValueObject.Position position)
        {
            return new Position
            {
                Id = position.Id,
                Code = position.Code,
                AliasTo = Tag.FromDomain(position.AliasTo)
            };
        }
    }
}
