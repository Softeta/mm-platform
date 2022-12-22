using Domain.Seedwork.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Email = EmailService.Sync.Events.Models.Email;
using Phone = EmailService.Sync.Events.Models.Phone;

namespace EmailService.Sync.Events.ContactPerson
{
    public class ContactPersonPayload
    {
        private readonly IReadOnlyDictionary<bool, int> _booleanValues = new ReadOnlyDictionary<bool, int>(
            new Dictionary<bool, int>()
            {
                { true, 1 },
                { false, 2 }
            });

        private readonly IReadOnlyDictionary<ContactPersonRole, int> _roleValues = new ReadOnlyDictionary<ContactPersonRole, int>(
            new Dictionary<ContactPersonRole, int>()
            {
                { ContactPersonRole.Admin, 1 },
                { ContactPersonRole.User, 2 }
            });

        public Email Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }

        public ContactPersonRole? Role { get; set; }
        public Phone? Phone { get; set; }
        public bool MarketingAcknowledgement { get; set; }

        public int MarketingAcknowledgementValue => _booleanValues[MarketingAcknowledgement];

        public int RoleValue => _roleValues[Role ?? ContactPersonRole.User];
    }
}
