using Domain.Seedwork.Enums;
using MediatR;
using System;

namespace EmailService.Send.Commands.Base
{
    public abstract class SendEmailBaseCommand : INotification
    {
        public string FilterName { get; set; } = null!;
        public Guid EntityId { get; set; }
        public Guid TargetId { get; set; }
        public string? EmailAddress { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }

        protected SendEmailBaseCommand(
           string filterName,
           Guid entityId,
           Guid targetId,
           string? emailAddress,
           SystemLanguage? systemLanguage)
        {
            FilterName = filterName;
            EntityId = entityId;
            TargetId = targetId;
            EmailAddress = emailAddress;
            SystemLanguage = systemLanguage;
        }
    }
}
