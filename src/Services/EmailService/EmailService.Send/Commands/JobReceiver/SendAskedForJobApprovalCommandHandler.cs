using Azure.Data.Tables;
using EmailService.Send.Constants;
using EmailService.Send.Events.JobShare;
using EmailService.Send.Models.EmailService;
using EmailService.Send.SendInBlue;
using EmailService.Shared.Enums;
using MediatR;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Commands.JobReceiver
{
    internal class SendAskedForJobApprovalCommandHandler : INotificationHandler<SendAskedForJobApprovalCommand>
    {
        private readonly TableClient _emailMessagesClient;
        private readonly ISmtpProvider _smtpProvider;

        public SendAskedForJobApprovalCommandHandler(IPrivateTableServiceClient client, ISmtpProvider smtpProvider)
        {
            _emailMessagesClient = client.GetTableClient(EmailMessageTableStorageConstants.TableName);
            _smtpProvider = smtpProvider;
        }

        public async Task Handle(SendAskedForJobApprovalCommand notification, CancellationToken cancellationToken)
        {
            if (notification.JobShareChangedEvent.Payload is null)
            {
                throw new InvalidDataContractException();
            }
            var payload = notification.JobShareChangedEvent.Payload;

            var emailInfo = GetEmailInfoData(payload);

            var messageId = await _smtpProvider.SendEmailAsync(emailInfo);

            var emailMessageTracker = new EmailMessageTrackerEntity
            {
                PartitionKey = EmailMessageTableStorageConstants.PartitionKey,
                RowKey = messageId,
                EntityId = payload.Id,
                TargetId = payload.AskedForJobApproval.Id,
                Email = payload.AskedForJobApproval.Email,
                Status = nameof(EmailStatus.sent),
                FilterName = notification.FilterName
            };

            await _emailMessagesClient.AddEntityAsync(emailMessageTracker, cancellationToken);
        }

        private static EmailInfo GetEmailInfoData(JobShareChangedPayload payload)
        {
            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.AskedForApprovalParameterKeys.Key, payload.Key }
            };

            var receivers = new List<Receiver>
            {
                new Receiver
                {
                    Email = payload.AskedForJobApproval.Email
                }
            };

            return new EmailInfo
            {
                Receivers = receivers,
                TemplateId = TemplateIds.AskedForApproval,
                Parameters = parameters
            };
        }
    }
}
