using Azure.Data.Tables;
using Domain.Seedwork.Exceptions;
using EmailService.Send.Commands.Base;
using EmailService.Send.Constants;
using EmailService.Send.Models.AppSettings;
using EmailService.Send.SendInBlue;
using Microsoft.Extensions.Options;
using Persistence.Customization.TableStorage.Clients;
using System.Collections.Generic;

namespace EmailService.Send.Commands.ContactPersonReceiver
{
    internal abstract class SendEmailToContactPersonBaseCommandHandler : SendEmailBaseCommandHandler
    {
        protected SendEmailToContactPersonBaseCommandHandler(
            IPrivateTableServiceClient client,
            ISmtpProvider smtpProvider,
            IOptions<TemplateOptions> options) : base(client, smtpProvider, options)
        {
        }

        protected void ValidateRequiredData(string? firstName, string? companyName, string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new NotFoundException("Email url not found.");
            }
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new NotFoundException("First name not found.");
            }
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new NotFoundException("Company name not found.");
            }
        }

        protected Dictionary<string, object> BuildParameters(string firstName, string companyName, string url)
        {
            var parameters = new Dictionary<string, object>
            {
                { ParameterKeys.EmailToContactPersonParameterKeys.Url, url },
                { ParameterKeys.EmailToContactPersonParameterKeys.ContactFirstName, firstName },
                { ParameterKeys.EmailToContactPersonParameterKeys.CompanyName, companyName }
            };

            return parameters;
        }
    }
}
