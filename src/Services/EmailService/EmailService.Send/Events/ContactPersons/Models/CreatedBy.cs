using Domain.Seedwork.Enums;
using System;

namespace EmailService.Send.Events.ContactPersons.Models
{
    internal class CreatedBy
    {
        public Guid Id { get; set; }
        public Scope Scope { get; set; }
    }
}
