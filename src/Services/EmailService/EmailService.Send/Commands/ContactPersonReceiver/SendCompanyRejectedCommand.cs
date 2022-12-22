using Domain.Seedwork.Enums;
using System;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    internal class SendCompanyRejectedCommand : SendEmailToContactPersonBaseCommand
    {
        public SendCompanyRejectedCommand(
            string filterName,
            Guid candidateId,
            string? emailAddress,
            SystemLanguage? systemLanguage,
            string? firstName,
            string? companyName) : base(filterName, candidateId, emailAddress, systemLanguage, firstName, companyName)
        {
        }
    }
}
