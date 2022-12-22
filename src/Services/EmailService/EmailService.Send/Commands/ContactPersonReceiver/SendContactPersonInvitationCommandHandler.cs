using EmailService.Send.Helpers;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.SendInBlue;
using MediatR;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage.Clients;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    internal class SendContactPersonInvitationCommandHandler<T> :
        SendEmailToContactPersonBaseCommandHandler,
        INotificationHandler<SendContactPersonInvitationCommand<T>> where T : EmailWithLinkOptions
    {
        private readonly string _url;
        public SendContactPersonInvitationCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<T> options) : base(client, smtpProvider, options)
        {
            _url = options.Value.Url;
        }

        public async Task Handle(SendContactPersonInvitationCommand<T> notification, CancellationToken cancellationToken)
        {
            ValidateRequiredData(notification.FirstName, notification.CompanyName, _url);
            var fullUrl = $"{_url}?{QueryParamsHelper.GetLanguage(notification.SystemLanguage)}";
            var parameters = BuildParameters(notification.FirstName!, notification.CompanyName!, fullUrl);
            var emailInfo = GetEmailInfoData(parameters, notification.EmailAddress, notification.SystemLanguage);

            await Handle(notification, emailInfo, cancellationToken);
        }
    }
}
