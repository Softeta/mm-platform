using Domain.Seedwork.Enums;
using EmailService.Send.Models.AppSettings;
using System;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    internal class SendContactPersonInvitationCommand<T> : SendEmailToContactPersonBaseCommand where T : EmailWithLinkOptions
    {
        public SendContactPersonInvitationCommand(
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
