namespace Domain.Seedwork;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot()
    {
        _domainEvents = new List<Event>();
    }

    private readonly List<Event> _domainEvents;
    public IReadOnlyCollection<Event> DomainEvents => _domainEvents.AsReadOnly();

    public void AddEvent(Event eventItem)
    {
        _domainEvents.Add(eventItem);
    }
}

