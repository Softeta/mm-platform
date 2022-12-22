using EmailService.Send.Events.ContactPersons;
using System.Collections.Generic;

namespace EmailService.Send.Events.Companies
{
    internal class CompanyChangedPayload
    {
        public IEnumerable<ContactPersonChangedPayload> ContactPersons { get; set; } = new List<ContactPersonChangedPayload>();
    }
}
