using Domain.Seedwork.Exceptions;
using EmailService.Send.Commands.Base;
using EmailService.Send.Constants;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.SendInBlue;
using MediatR;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage.Clients;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    public class SendJobSubmittedCommandHandler :
        SendEmailBaseCommandHandler,
        INotificationHandler<SendJobSubmittedCommand>
    {
        public SendJobSubmittedCommandHandler(IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<JobSubmittedOptions> options)
            : base(client, smtpProvider, options)
        {
        }

        public async Task Handle(SendJobSubmittedCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification);
            var parameters = BuildParameters(notification);

            var emailInfo = GetEmailInfoData(
                parameters,
                notification.EmailAddress,
                notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }

        private Dictionary<string, object> BuildParameters(SendJobSubmittedCommand notification)
        {
            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.JobSubmittedParameterKeys.ContactFirstName, notification.ContactPersonFirstName! },
                { ParameterKeys.JobSubmittedParameterKeys.JobPosition, notification.JobPosition! }
            };

            return parameters;
        }

        private void ValidateRequiredData(SendJobSubmittedCommand notification)
        {
            if (string.IsNullOrWhiteSpace(notification.ContactPersonFirstName))
            {
                throw new NotFoundException("First name of candidate not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.JobPosition))
            {
                throw new NotFoundException("Position of job not found.");
            }
        }
    }
}
