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
    public class SendJobRejectedCommandHandler :
        SendEmailBaseCommandHandler,
        INotificationHandler<SendJobRejectedCommand>
    {
        public SendJobRejectedCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<JobRejectedOptions> options)
            : base(client, smtpProvider, options)
        {
        }

        public async Task Handle(SendJobRejectedCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification);
            var parameters = BuildParameters(notification);

            var emailInfo = GetEmailInfoData(
                parameters,
                notification.EmailAddress,
                notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }

        private Dictionary<string, object> BuildParameters(SendJobRejectedCommand notification)
        {
            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.JobRejectedParameterKeys.ContactFirstName, notification.ContactPersonFirstName! },
                { ParameterKeys.JobRejectedParameterKeys.JobPosition, notification.JobPosition! }
            };

            return parameters;
        }

        private void ValidateRequiredData(SendJobRejectedCommand notification)
        {
            if (string.IsNullOrWhiteSpace(notification.ContactPersonFirstName))
            {
                throw new NotFoundException("First name of contact person not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.JobPosition))
            {
                throw new NotFoundException("Position of job not found.");
            }
        }
    }
}
