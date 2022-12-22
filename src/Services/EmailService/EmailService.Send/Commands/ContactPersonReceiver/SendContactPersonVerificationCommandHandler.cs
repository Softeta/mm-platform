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

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    internal class SendContactPersonVerificationCommandHandler : 
        SendVerificationEmailBaseCommandHandler,
        INotificationHandler<SendContactPersonVerificationCommand>
    {
        private readonly EmailWithLinkOptions _options;

        public SendContactPersonVerificationCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider, 
            IOptions<ContactPersonVerificationOptions> options) : base(client, smtpProvider, options)
        {
            _options = options.Value;
        }

        public async Task Handle(SendContactPersonVerificationCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification);

            var parameters = BuildParameters(notification);
            var emailInfo = GetEmailInfoData(parameters, notification.EmailAddress, notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }

        private Dictionary<string, object> BuildParameters(SendContactPersonVerificationCommand notification)
        {
            var fullVerificationUrl = BuildFullVerificationAddress(
                notification.CompanyId,
                notification.UserId,
                notification.VerificationKey!.Value);

            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.EmailVerificationParameterKeys.Url, fullVerificationUrl }
            };

            return parameters;
        }

        private string BuildFullVerificationAddress(Guid companyId, Guid contactId, Guid verificationKey)
            => string.Format(_options.Url, companyId, contactId, verificationKey);
    }
}
