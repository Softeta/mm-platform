using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using System;

namespace EmailService.Send.Commands.CandidateReceiver
{
    internal class SendCandidateVerificationCommand : SendVerificationEmailBaseCommand
    {
        public SendCandidateVerificationCommand(
            string filterName,
            Guid candidateId,
            string? emailAddress,
            Guid? verificationKey,
            SystemLanguage? systemLanguage) : base(filterName, candidateId, emailAddress, verificationKey, systemLanguage)
        {
        }
    }
}
