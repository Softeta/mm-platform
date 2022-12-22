using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.Publishers;

namespace Companies.Application.EventBus.Publishers
{
    public class CompanyEventBusPublisher : EventBusPublisher, ICompanyEventBusPublisher
    {
        public CompanyEventBusPublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Topics.CompanyChanged.Name)
        {
        }
    }
}
