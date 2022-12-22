using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.Publishers;

namespace Jobs.Application.EventBus.Publishers
{
    public class JobEventBusPublisher : EventBusPublisher, IJobEventBusPublisher
    {
        public JobEventBusPublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Topics.JobChanged.Name)
        {
        }
    }
}
