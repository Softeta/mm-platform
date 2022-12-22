using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using System;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    public class SendJobSubmittedCommand : SendEmailBaseCommand
    {
        public SendJobSubmittedCommand(
           string filterName,
           Guid jobId,
           string? jobPosition,
           Guid contactPersonId,
           string? contactPersonFirstName,
           string? contactPersonEmail,
           SystemLanguage? systemLanguage) : base(filterName, jobId, contactPersonId, contactPersonEmail, systemLanguage)
        {
            JobPosition = jobPosition;
            ContactPersonFirstName = contactPersonFirstName;
        }

        public string? JobPosition { get; set; }
        public string? ContactPersonFirstName { get; set; }
    }
}
