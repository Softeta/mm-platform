using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using System;

namespace EmailService.Send.Commands.CandidateReceiver
{
    public class SendCandidateRejectedCommand : SendEmailBaseCommand
    {
        public SendCandidateRejectedCommand(
            string filterName,
            Guid candidateId,
            string? emailAddress,
            SystemLanguage? systemLanguage) : base(filterName, candidateId, candidateId, emailAddress, systemLanguage)
        {
        }
    }
}
