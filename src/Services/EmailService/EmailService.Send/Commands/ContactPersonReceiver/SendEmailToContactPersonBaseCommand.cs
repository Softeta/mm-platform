using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using System;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    internal abstract class SendEmailToContactPersonBaseCommand : SendEmailBaseCommand
    {
        public string? FirstName { get; set; }
        public string? CompanyName { get; set; }

        protected SendEmailToContactPersonBaseCommand(
            string filterName,
            Guid contactId,
            string? emailAddress,
            SystemLanguage? systemLanguage,
            string? firstName,
            string? companyName) : base(filterName, contactId, contactId, emailAddress, systemLanguage)
        {
            FirstName = firstName;
            CompanyName = companyName;
        }
    }
}
