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

namespace EmailService.Send.Commands.CandidateReceiver
{
    internal class SendCandidateAppliedToJobCommandHandler :
        SendEmailBaseCommandHandler,
        INotificationHandler<SendCandidateAppliedToJobCommand>
    {
        private readonly CandidateAppliedToJobOptions _options;

        public SendCandidateAppliedToJobCommandHandler(
            IPrivateTableServiceClient client, 
            ISmtpProvider smtpProvider,
            IOptions<CandidateAppliedToJobOptions> options) : base(client, smtpProvider, options)
        {
            _options = options.Value;
        }

        public async Task Handle(SendCandidateAppliedToJobCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification);
            var parameters = BuildParameters(notification);

            var emailInfo = GetEmailInfoData(
                parameters,
                notification.EmailAddress,
                notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }

        private Dictionary<string, object> BuildParameters(SendCandidateAppliedToJobCommand notification)
        {
            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.CandidateAppliedToJobParameterKeys.CandidateFirstName, notification.FirstName! },
                { ParameterKeys.CandidateAppliedToJobParameterKeys.CompanyName, notification.AppliedToJob.Company!.Name! },
                { ParameterKeys.CandidateAppliedToJobParameterKeys.JobPosition, notification.AppliedToJob.Position!.Code! },
            };

            return parameters;
        }

        private void ValidateRequiredData(SendCandidateAppliedToJobCommand notification)
        {
          
            if (string.IsNullOrWhiteSpace(notification.AppliedToJob.Position?.Code))
            {
                throw new NotFoundException("Position of job not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.AppliedToJob.Company?.Name))
            {
                throw new NotFoundException("Company name of job not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.FirstName))
            {
                throw new NotFoundException("First name of candidate not found.");
            }
        }
    }
}
