using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using System;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    public class SendJobApprovedCommand : SendEmailBaseCommand
    {
        public SendJobApprovedCommand(
            string filterName,
            Guid jobId,
            string? jobPosition,
            string? compantName,
            Guid contactPersonId,
            string? contactPersonFirstName,
            string? contactPersonEmail,
            SystemLanguage? systemLanguage) : base(filterName, jobId, contactPersonId, contactPersonEmail, systemLanguage)
        {
            JobPosition = jobPosition;
            CompantName = compantName;
            ContactPersonFirstName = contactPersonFirstName;
        }

        public string? JobPosition { get; set; }
        public string? CompantName { get; set; }
        public string? ContactPersonFirstName { get; set; }
    }
}