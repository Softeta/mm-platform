using DomainValueObject = Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;

        public static Tag? FromDomain(DomainValueObject.Tag? tag)
        {
            if (tag is null) return null;

            return new Tag
            {
                Id = tag.Id,
                Code = tag.Code
            };
        }
    }
}
