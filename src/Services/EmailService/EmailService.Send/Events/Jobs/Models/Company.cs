using System.Collections.Generic;

namespace EmailService.Send.Events.Jobs.Models
{
    internal class Company
    {
        public string Name { get; set; } = null!;
        public IEnumerable<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();
    }
}
