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
    public class SendJobApprovedCommandHandler :
        SendEmailBaseCommandHandler,
        INotificationHandler<SendJobApprovedCommand>
    {
        public SendJobApprovedCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<JobApprovedOptions> options)
            : base(client, smtpProvider, options)
        {
        }

        public async Task Handle(SendJobApprovedCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification);
            var parameters = BuildParameters(notification);

            var emailInfo = GetEmailInfoData(
                parameters,
                notification.EmailAddress,
                notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }

        private Dictionary<string, object> BuildParameters(SendJobApprovedCommand notification)
        {
            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.JobApprovedParameterKeys.ContactFirstName, notification.ContactPersonFirstName! },
                { ParameterKeys.JobApprovedParameterKeys.CompanyName, notification.CompantName! },
                { ParameterKeys.JobApprovedParameterKeys.JobPosition, notification.JobPosition! },
            };

            return parameters;
        }

        private void ValidateRequiredData(SendJobApprovedCommand notification)
        {
            if (string.IsNullOrWhiteSpace(notification.ContactPersonFirstName))
            {
                throw new NotFoundException("First name of contact person not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.CompantName))
            {
                throw new NotFoundException("Company name of job not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.JobPosition))
            {
                throw new NotFoundException("Position of job not found.");
            }
        }
    }
}
