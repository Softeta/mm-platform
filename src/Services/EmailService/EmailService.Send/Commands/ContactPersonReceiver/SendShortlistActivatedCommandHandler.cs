using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using EmailService.Send.Commands.Base;
using EmailService.Send.Constants;
using EmailService.Send.Helpers;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.SendInBlue;
using MediatR;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage.Clients;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    public class SendShortlistActivatedCommandHandler :
        SendEmailBaseCommandHandler,
        INotificationHandler<SendShortlistActivatedCommand>
    {
        private readonly JobCandidatesShortlistActivatedOptions _options;

        public SendShortlistActivatedCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<JobCandidatesShortlistActivatedOptions> options) : base(client, smtpProvider, options)
        {
            _options = options.Value;
        }

        public async Task Handle(SendShortlistActivatedCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification);
            var parameters = BuildParameters(notification);

            var emailInfo = GetEmailInfoData(
                parameters,
                notification.EmailAddress,
                notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }

        private Dictionary<string, object> BuildParameters(SendShortlistActivatedCommand notification)
        {
            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.JobCandidatesShortlistActivatedParameterKeys.Url, GetUrl(
                    notification.IsContactRegistered,
                    notification.EntityId,
                    notification.SystemLanguage) },
                { ParameterKeys.JobCandidatesShortlistActivatedParameterKeys.JobPosition, notification.JobPosition! },
                { ParameterKeys.JobCandidatesShortlistActivatedParameterKeys.ContactFirstName, notification.ContactFirstName! }
            };

            return parameters;
        }

        private void ValidateRequiredData(SendShortlistActivatedCommand notification)
        {
            if (string.IsNullOrWhiteSpace(notification.JobPosition))
            {
                throw new NotFoundException("Position of job not found");
            }
            if (string.IsNullOrWhiteSpace(notification.ContactFirstName))
            {
                throw new NotFoundException("Contact first name not found");
            }
            if (notification.EntityId == default)
            {
                throw new NotFoundException("Job id not found");
            }
        }

        private string GetUrl(bool isRegistered, Guid jobId, SystemLanguage? systemLanguage)
        {
            if (isRegistered)
            {
                return $"{_options.Url.TrimEnd('/')}/{jobId}";
            }
            return $"{_options.SignupUrl.TrimEnd('/')}?{QueryParamsHelper.GetLanguage(systemLanguage)}";
        }
    }
}
