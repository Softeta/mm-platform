using Azure.Data.Tables;
using EmailService.Send.Commands.Base;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.SendInBlue;
using MediatR;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage.Clients;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Commands.CandidateReceiver
{
    internal class SendCandidateRejectedCommandHandler :
        SendEmailBaseCommandHandler,
        INotificationHandler<SendCandidateRejectedCommand>
    {
        public SendCandidateRejectedCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<CandidateRejectedOptions> options) 
            : base(client, smtpProvider, options)
        {
        }

        public async Task Handle(SendCandidateRejectedCommand notification, CancellationToken cancellationToken)
        {
            var emailInfo = GetEmailInfoData(
                new Dictionary<string, object>(),
                notification.EmailAddress,
                notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }
    }
}
