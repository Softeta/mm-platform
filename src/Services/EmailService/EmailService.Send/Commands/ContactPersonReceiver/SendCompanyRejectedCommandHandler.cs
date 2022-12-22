using Azure.Data.Tables;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.SendInBlue;
using MediatR;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage.Clients;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    internal class SendCompanyRejectedCommandHandler :
        SendEmailToContactPersonBaseCommandHandler,
        INotificationHandler<SendCompanyRejectedCommand>
    {
        private readonly string _url;
        public SendCompanyRejectedCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<CompanyRejectedOptions> options) : base(client, smtpProvider, options)
        {
            _url = options.Value.Url;
        }

        public async Task Handle(SendCompanyRejectedCommand notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification.FirstName, notification.CompanyName, _url);

            var parameters = BuildParameters(notification.FirstName!, notification.CompanyName!, _url);
            var emailInfo = GetEmailInfoData(parameters, notification.EmailAddress, notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }
    }
}
