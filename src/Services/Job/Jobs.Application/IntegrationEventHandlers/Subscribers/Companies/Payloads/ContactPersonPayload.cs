using Domain.Seedwork.Enums;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Companies.Payloads.Models;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies.Payloads
{
    public class ContactPersonPayload
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Email Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Position? Position { get; set; }
        public Phone? Phone { get; set; }
        public string? PictureUri { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
        public Guid? ExternalId { get; set; }
    }
}
