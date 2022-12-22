using EmailService.Send.Models.EmailService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sib = sib_api_v3_sdk;

namespace EmailService.Send.SendInBlue
{
    public class SendInBlueClient : ISmtpProvider
    {
        private const string apiKeyName = "api-key";
        private readonly Sib.Client.Configuration _configurations;
      
        public SendInBlueClient(string apiKey)
        {
            _configurations = new Sib.Client.Configuration();
            _configurations.ApiKey.Add(apiKeyName, apiKey);
        }

        public async Task<string> SendEmailAsync(EmailInfo emailInfo)
        {   
            var transactionalApi = new Sib.Api.TransactionalEmailsApi(_configurations);

            var email = new Sib.Model.SendSmtpEmail(); 
            email.TemplateId = emailInfo.TemplateId;
            email.Params = emailInfo.Parameters;
            email.To = new List<Sib.Model.SendSmtpEmailTo>();

            foreach(var receiver in emailInfo.Receivers)
            {
                var sendSmtpEmailTo = new Sib.Model.SendSmtpEmailTo(receiver.Email, receiver.Name);
                email.To.Add(sendSmtpEmailTo);
            }

            var result = await transactionalApi.SendTransacEmailAsync(email);

            return result.MessageId;
        }
    }
}
