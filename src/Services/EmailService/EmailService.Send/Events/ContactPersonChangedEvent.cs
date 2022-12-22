using EmailService.Send.Events.ContactPersons;
using EventBus.Events;

namespace EmailService.Send.Events
{
    internal class ContactPersonChangedEvent : Event<ContactPersonChangedPayload>
    {
    }
}
