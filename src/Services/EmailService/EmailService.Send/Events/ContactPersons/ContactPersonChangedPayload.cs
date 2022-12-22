using Domain.Seedwork.Enums;
using EmailService.Send.Events.Candidates.Models;
using EmailService.Send.Events.ContactPersons.Models;
using System;

namespace EmailService.Send.Events.ContactPersons
{
    internal class ContactPersonChangedPayload
    {
        public Guid CompanyId { get; set; }
        public Guid Id { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
        public Email Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? CompanyName { get; set; }
        public CreatedBy? CreatedBy { get; set; }
    }
}
