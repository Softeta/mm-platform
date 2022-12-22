using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using System;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    internal class SendContactPersonVerificationCommand : SendVerificationEmailBaseCommand
    {
        public SendContactPersonVerificationCommand(
            string filterName,
            Guid companyId,
            Guid contactId,
            string? emailAddress,
            Guid? verificationKey,
            SystemLanguage? systemLanguage) : base(filterName, contactId, emailAddress, verificationKey, systemLanguage)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; set; }
    }
}
