using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.Publishers;

namespace Candidates.Application.EventBus.Publishers
{
    public class CandidateJobsEventBusPublisher : EventBusPublisher, ICandidateJobsEventBusPublisher
    {
        public CandidateJobsEventBusPublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Topics.CandidateJobsChanged.Name)
        {
        }
    }
}
