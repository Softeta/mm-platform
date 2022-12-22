namespace ElasticSearch.Sync.Events.Models.Shared
{
    internal class Position
    {
        public string Code { get; set; } = null!;
        public Alias? AliasTo { get; set; }
    }
}
