using EmailService.Sync.Events.ContactPerson;
using EventBus.Events;

namespace EmailService.Sync.Events
{
    internal class ContactPersonChangedEvent : Event<ContactPersonPayload>
    {
    }
}
