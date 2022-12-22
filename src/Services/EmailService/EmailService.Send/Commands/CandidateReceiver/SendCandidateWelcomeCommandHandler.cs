using Azure.Data.Tables;
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

namespace EmailService.Send.Commands.CandidateReceiver
{
    internal class SendCandidateWelcomeCommandHandler : 
        SendEmailBaseCommandHandler,
        INotificationHandler<SendCandidateWelcomeCommand>
    {
        private readonly CandidateWelcomeOptions _options;

        public SendCandidateWelcomeCommandHandler(
            IPrivateTableServiceClient client, 
            ISmtpProvider smtpProvider,
            IOptions<CandidateWelcomeOptions> options) : base(client, smtpProvider, options)
        {
            _options = options.Value;
        }

        public async Task Handle(SendCandidateWelcomeCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification);
            var parameters = BuildParameters(notification);
            var emailInfo = GetEmailInfoData(parameters, notification.EmailAddress, notification.SystemLanguage);
            await Handle(notification, emailInfo, cancellationToken);
        }

        private void ValidateRequiredData(SendCandidateWelcomeCommand notification)
        {
            if (string.IsNullOrWhiteSpace(_options.Url))
            {
                throw new NotFoundException("Email url not found.");
            }
            if (string.IsNullOrWhiteSpace(notification.FirstName))
            {
                throw new NotFoundException("First name of candidate not found.");
            }
        }

        private Dictionary<string, object> BuildParameters(SendCandidateWelcomeCommand notification)
        {
            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.CandidateWelcomeParameterKeys.Url, _options.Url },
                { ParameterKeys.CandidateWelcomeParameterKeys.CandidateFirstName, notification.FirstName! }
            };

            return parameters;
        }
    }
}
