using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.Publishers;

namespace Companies.Application.EventBus.Publishers
{
    public class ContactPersonEventBusPublisher : EventBusPublisher, IContactPersonEventBusPublisher
    {
        public ContactPersonEventBusPublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Topics.ContactPersonChanged.Name)
        {
        }
    }
}
