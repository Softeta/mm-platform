using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using EmailService.Send.Commands.Base;
using EmailService.Send.Constants;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.SendInBlue;
using MediatR;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Commands.JobCandidateReceiver
{
    internal class SendJobCandidateInvitedCommandHandler :
        SendEmailBaseCommandHandler,
        INotificationHandler<SendJobCandidateInvitedCommand>
    {
        private readonly JobCandidateInvitedOptions _options;

        public SendJobCandidateInvitedCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<JobCandidateInvitedOptions> options)
            : base(client, smtpProvider, options)
        {
            _options = options.Value;
        }

        public async Task Handle(SendJobCandidateInvitedCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification);
            var parameters = BuildParameters(notification);

            var emailInfo = GetEmailInfoData(
                parameters,
                notification.EmailAddress,
                notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }

        private Dictionary<string, object> BuildParameters(SendJobCandidateInvitedCommand notification)
        {
            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.JobCandidateInvitedParameterKeys.Url, _options.Url },
                { ParameterKeys.JobCandidateInvitedParameterKeys.CandidateFirstName, notification.Payload.FirstName! },
                { ParameterKeys.JobCandidateInvitedParameterKeys.CompanyName, notification.Payload.Job!.Company!.Name! },
                { ParameterKeys.JobCandidateInvitedParameterKeys.JobPosition, notification.Payload.Job!.Position!.Code! },
            };

            return parameters;
        }

        private void ValidateRequiredData(SendJobCandidateInvitedCommand notification)
        {
            if (string.IsNullOrWhiteSpace(_options.Url))
            {
                throw new NotFoundException("Email url not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.Payload.FirstName))
            {
                throw new NotFoundException("First name of candidate not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.Payload.Job?.Company?.Name))
            {
                throw new NotFoundException("Company name of job not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.Payload.Job?.Position?.Code))
            {
                throw new NotFoundException("Position of job not found.");
            }
        }
    }
}
