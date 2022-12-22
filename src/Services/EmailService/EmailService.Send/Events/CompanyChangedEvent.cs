using EmailService.Send.Events.Companies;
using EventBus.Events;

namespace EmailService.Send.Events
{
    internal class CompanyChangedEvent : Event<CompanyChangedPayload>
    {
    }
}
