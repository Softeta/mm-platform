using MediatR;

namespace Domain.Seedwork;

public abstract class Event : INotification
{
    public bool Published { get; private set; }

    public void MarkAsPublished()
    {
        Published = true;
    }
}
