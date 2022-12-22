namespace EventBus.Events
{
    public abstract class Event<T> where T : class
    {
        public T? Payload { get; set; }
    }
}
