using Azure.Data.Tables;
using Domain.Seedwork.Exceptions;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.SendInBlue;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage.Clients;

namespace EmailService.Send.Commands.Base
{
    internal abstract class SendVerificationEmailBaseCommandHandler : SendEmailBaseCommandHandler
    {
        private readonly EmailWithLinkOptions _options;

        protected SendVerificationEmailBaseCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider, 
            IOptions<EmailWithLinkOptions> options) : base(client, smtpProvider, options)
        {
            _options = options.Value;
        }

        protected void ValidateRequiredData(SendVerificationEmailBaseCommand notification)
        {
            if (notification.VerificationKey is null)
            {
                throw new NotFoundException("Email verification key not found", ErrorCodes.NotFound.EmailVerificationKeyNotFound);
            }
            if (string.IsNullOrWhiteSpace(_options.Url))
            {
                throw new NotFoundException("Email verification url not found.", ErrorCodes.NotFound.EmailLinkNotFound);
            }
        }
    }
}
