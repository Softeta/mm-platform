using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.Publishers;

namespace EmailService.WebHook.EventBus.Publishers
{
    public class EmailServiceWebHookEventBusPublisher : EventBusPublisher, IEmailServiceWebHookEventBusPublisher
    {
        public EmailServiceWebHookEventBusPublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Topics.EmailServiceWebHooked.Name)
        {
        }
    }
}
