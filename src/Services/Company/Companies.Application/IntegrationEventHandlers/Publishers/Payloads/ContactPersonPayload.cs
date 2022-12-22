using Companies.Application.IntegrationEventHandlers.Publishers.Payloads.Models.ContactPerson;
using Companies.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Domain.Seedwork.Enums;

namespace Companies.Application.IntegrationEventHandlers.Publishers.Payloads
{
    internal class ContactPersonPayload
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public Guid? ExternalId { get; set; }
        public Email Email { get; set; } = null!;
        public ContactPersonStage Stage { get; set; }
        public ContactPersonRole Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Position? Position { get; set; }
        public Phone? Phone { get; set; }
        public string? PictureUri { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
        public Image? Picture { get; set; } 
        public bool MarketingAcknowledgement { get; set; }
        public CreatedBy? CreatedBy { get; set; }

        public static ContactPersonPayload FromDomain(Company company, ContactPerson contact)
        {
            return new ContactPersonPayload
            {
                Id = contact.Id,
                CompanyId = contact.CompanyId,
                CompanyName = company.Name,
                ExternalId = contact.ExternalId,
                Email = Email.FromDomain(contact.Email),
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PictureUri = contact.Picture?.ThumbnailUri,
                Position = Position.FromDomain(contact.Position),
                Phone = new Phone(contact.Phone?.PhoneNumber),
                Stage = contact.Stage,
                CreatedAt = contact.CreatedAt,
                SystemLanguage = contact.SystemLanguage,
                Picture = Image.FromDomain(contact.Picture),
                MarketingAcknowledgement = contact.MarketingAcknowledgement?.Agreed ?? false,
                Role = contact.Role,
                CreatedBy = CreatedBy.FromDomain(contact.CreatedBy)
            };
        }
    }
}
