using Domain.Seedwork.Enums;
using System;

namespace EmailService.Send.Events.Jobs.Models
{
    internal class ContactPerson
    {
        public Guid PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
    }
}
