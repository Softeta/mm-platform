using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using System;

namespace EmailService.Send.Commands.CandidateReceiver
{
    internal class SendCandidateWelcomeCommand : SendEmailBaseCommand
    {
        public SendCandidateWelcomeCommand(
            string filterName,
            Guid candidateId,
            string? emailAddress,
            string? firstName,
            SystemLanguage? systemLanguage) : base(filterName, candidateId, candidateId, emailAddress, systemLanguage)
        {
            FirstName = firstName;
        }

        public string? FirstName { get; set; }
    }
}
