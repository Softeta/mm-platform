using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.Publishers;

namespace Scheduler.Job.EventBus.Publishers
{
    public class SchedulerJobEventBusPublisher : EventBusPublisher, ISchedulerJobEventBusPublisher
    {
        public SchedulerJobEventBusPublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Topics.SchedulerJobScheduled.Name)
        {
        }
    }
}
