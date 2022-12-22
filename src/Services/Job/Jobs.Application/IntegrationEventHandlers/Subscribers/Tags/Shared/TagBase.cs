namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.Shared
{
    public abstract class TagBase
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public Alias? AliasTo { get; set; }
    }
}
