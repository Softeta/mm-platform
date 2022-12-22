using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class ContactPerson
    {
        public Guid PersonId { get; set; }
        public bool IsMainContact { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public string? PictureUri { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }

        public static ContactPerson FromDomain(JobContactPerson jobContactPerson)
        {
            return new ContactPerson
            {
                PersonId = jobContactPerson.PersonId,
                FirstName = jobContactPerson.FirstName,
                LastName = jobContactPerson.LastName,
                PhoneNumber = jobContactPerson.PhoneNumber,
                Email = jobContactPerson.Email,
                PictureUri = jobContactPerson.PictureUri,
                IsMainContact = jobContactPerson.IsMainContact,
                SystemLanguage = jobContactPerson.SystemLanguage
            };
        }
    }
}
