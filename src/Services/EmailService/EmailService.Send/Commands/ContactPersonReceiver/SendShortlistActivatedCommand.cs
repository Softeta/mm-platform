using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using System;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    public class SendShortlistActivatedCommand : SendEmailBaseCommand
    {
        public SendShortlistActivatedCommand(
            string filterName, 
            Guid JobId,
            string? jobPosition,
            string? contactEmailAddress,
            string? contactFirstName,
            SystemLanguage? systemLanguage,
            bool isContactRegistered) : base(filterName, JobId, JobId, contactEmailAddress, systemLanguage)
        {
            JobPosition = jobPosition;
            ContactFirstName = contactFirstName;
            IsContactRegistered = isContactRegistered;
        }

        public string? ContactFirstName { get; set; }
        public string? JobPosition { get; set; }
        public bool IsContactRegistered { get; set; }
    }
}
