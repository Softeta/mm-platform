using Azure.Data.Tables;
using EmailService.Send.Commands.Base;
using EmailService.Send.Constants;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.SendInBlue;
using MediatR;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage.Clients;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Commands.CandidateReceiver
{
    internal class SendCandidateVerificationCommandHandler :
        SendVerificationEmailBaseCommandHandler,
        INotificationHandler<SendCandidateVerificationCommand>
    {
        private readonly EmailWithLinkOptions _options;

        public SendCandidateVerificationCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider, 
            IOptions<CandidateVerificationOptions> options) : base(client, smtpProvider, options)
        {
            _options = options.Value;
        }

        public async Task Handle(SendCandidateVerificationCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification);

            var parameters = BuildParameters(notification);
            var emailInfo = GetEmailInfoData(parameters, notification.EmailAddress, notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }

        private Dictionary<string, object> BuildParameters(SendCandidateVerificationCommand notification)
        {
            var fullVerificationUrl = BuildFullVerificationAddress(
                notification.UserId,
                notification.VerificationKey!.Value);

            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.EmailVerificationParameterKeys.Url, fullVerificationUrl }
            };

            return parameters;
        }

        private string BuildFullVerificationAddress(Guid candidateId, Guid verificationKey)
            => string.Format(_options.Url, candidateId, verificationKey);
    }
}
