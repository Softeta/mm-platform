using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared
{
    internal class Tag
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;

        public static Tag? FromDomain(ValueObjects.Tag? tag)
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
