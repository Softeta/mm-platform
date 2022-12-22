using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.Publishers;

namespace Jobs.Application.EventBus.Publishers
{
    public class JobCandidatesEventBusPublisher : EventBusPublisher, IJobCandidatesEventBusPublisher
    {
        public JobCandidatesEventBusPublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Topics.JobCandidatesChanged.Name)
        {
        }
    }
}
