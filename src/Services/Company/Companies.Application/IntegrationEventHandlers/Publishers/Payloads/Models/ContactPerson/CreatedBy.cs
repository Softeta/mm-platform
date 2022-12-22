using Domain.Seedwork.Enums;
using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Companies.Application.IntegrationEventHandlers.Publishers.Payloads.Models.ContactPerson
{
    internal class CreatedBy
    {
        public Guid Id { get; set; }
        public Scope Scope { get; set; }

        public static CreatedBy? FromDomain(ValueObjects.CreatedBy? createdBy)
        {
            if (createdBy is null) return null;

            return new CreatedBy
            {
                Id = createdBy.Id,
                Scope = createdBy.Scope
            };
        }
    }
}
