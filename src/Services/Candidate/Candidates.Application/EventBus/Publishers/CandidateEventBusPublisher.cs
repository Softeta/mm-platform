using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.Publishers;

namespace Candidates.Application.EventBus.Publishers
{
    public class CandidateEventBusPublisher : EventBusPublisher, ICandidateEventBusPublisher
    {
        public CandidateEventBusPublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Topics.CandidateChanged.Name)
        {
        }
    }
}
