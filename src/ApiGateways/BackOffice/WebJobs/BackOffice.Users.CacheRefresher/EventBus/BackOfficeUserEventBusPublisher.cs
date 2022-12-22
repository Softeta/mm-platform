using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.Publishers;

namespace BackOffice.Users.CacheRefresher.EventBus
{
    public class BackOfficeUserEventBusPublisher : EventBusPublisher, IBackOfficeUserEventBusPublisher
    {
        public BackOfficeUserEventBusPublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Topics.BackOfficeUserChanged.Name)
        {
        }
    }
}
