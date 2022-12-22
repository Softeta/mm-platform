using Domain.Seedwork.Enums;
using MediatR;
using System;

namespace EmailService.Send.Commands.Base
{
    internal abstract class SendVerificationEmailBaseCommand : SendEmailBaseCommand
    {
        protected SendVerificationEmailBaseCommand(
            string filterName, 
            Guid userId,
            string? emailAddress,
            Guid? verificationKey,
            SystemLanguage? systemLanguage) : base(filterName, userId, userId, emailAddress, systemLanguage)
        {
            UserId = userId;
            VerificationKey = verificationKey;
        }
        public Guid UserId { get; set; }
        public Guid? VerificationKey { get; set; }
    }
}
