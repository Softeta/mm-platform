using Domain.Seedwork.Enums;
using System;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    internal class SendContactPersonWelcomeCommand : SendEmailToContactPersonBaseCommand
    {
        public SendContactPersonWelcomeCommand(
            string filterName, 
            Guid contactId,
            string? emailAddress,
            SystemLanguage? systemLanguage,
            string? firstName,
            string? companyName) : base(filterName, contactId, emailAddress, systemLanguage, firstName, companyName)
        {
        }
    }
}
