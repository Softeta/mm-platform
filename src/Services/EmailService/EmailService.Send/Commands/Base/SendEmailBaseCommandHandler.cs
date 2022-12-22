using Azure.Data.Tables;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.Models.EmailService;
using EmailService.Send.SendInBlue;
using EmailService.Shared.Enums;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Commands.Base
{
    public abstract class SendEmailBaseCommandHandler
    {
        private readonly TableClient _emailMessagesClient;
        private readonly ISmtpProvider _smtpProvider;
        private readonly TemplateOptions _options;

        public SendEmailBaseCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<TemplateOptions> options)
        {
            _emailMessagesClient = client.GetTableClient(EmailMessageTableStorageConstants.TableName);
            _smtpProvider = smtpProvider;
            _options = options.Value;
        }

        protected async Task Handle(SendEmailBaseCommand notification, EmailInfo info, CancellationToken cancellationToken)
        {
            var messageId = await _smtpProvider.SendEmailAsync(info);

            var emailMessageTracker = new EmailMessageTrackerEntity
            {
                PartitionKey = EmailMessageTableStorageConstants.PartitionKey,
                RowKey = messageId,
                EntityId = notification.EntityId,
                TargetId = notification.TargetId,
                Email = notification.EmailAddress!,
                Status = nameof(EmailStatus.sent),
                FilterName = notification.FilterName
            };

            await _emailMessagesClient.AddEntityAsync(emailMessageTracker, cancellationToken);
        }

        protected virtual EmailInfo GetEmailInfoData
           (Dictionary<string, object> parameters,
           string? email,
           SystemLanguage? systemLanguage)
        {
            ValidateEmailAddress(email);

            var receivers = new List<Receiver>
            {
                new Receiver
                {
                    Email = email!
                }
            };

            return new EmailInfo
            {
                Receivers = receivers,
                TemplateId = GetTemplateIdBySystemLanguage(systemLanguage),
                Parameters = parameters
            };
        }

        private long GetTemplateIdBySystemLanguage(SystemLanguage? systemLanguage)
        {
            switch (systemLanguage)
            {
                case SystemLanguage.EN:
                    return _options.TemplateId.EN;
                case SystemLanguage.DA:
                    return _options.TemplateId.DA;
                case SystemLanguage.SV:
                    return _options.TemplateId.SV;
                default:
                    return _options.TemplateId.DA;
            }
        }

        private static void ValidateEmailAddress(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new NotFoundException("Email address not found.", ErrorCodes.NotFound.EmailNotFound);
            }
        }
    }
}
